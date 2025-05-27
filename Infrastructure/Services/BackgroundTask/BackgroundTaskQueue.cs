using Application.Common.Models;
using Application.Interfaces;
using Infrastructure.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure.Services.BackgroundTask;

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<WorkItem> _queue;
    private ICollection<WorkItem> _workingItems;

    private readonly IHubContext<JobHub> _job;
    public BackgroundTaskQueue(int capacity, IHubContext<JobHub> job)
    {
        _job = job;

        BoundedChannelOptions options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<WorkItem>(options);
        _workingItems = new List<WorkItem>();
    }

    public async ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> action, string id = null)
    {
        Func<CancellationToken, IServiceProvider, Action<decimal, object>, ValueTask> workItemService = (cancellationToken, provider, updatePercent) => action(cancellationToken);
        await this.QueueBackgroundWorkItemWithProgressAsync(workItemService, id);
    }

    public async ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, IServiceProvider, ValueTask> action, string id = null)
    {
        Func<CancellationToken, IServiceProvider, Action<decimal, object>, ValueTask> workItemService = (cancellationToken, provider, updatePercent) => action(cancellationToken, provider);
        await this.QueueBackgroundWorkItemWithProgressAsync(workItemService, id);
    }

    public async ValueTask QueueBackgroundWorkItemWithProgressAsync(Func<CancellationToken, Action<decimal, object>, ValueTask> action, string id = null)
    {
        Func<CancellationToken, IServiceProvider, Action<decimal, object>, ValueTask> workItemService = (cancellationToken, provider, updatePercent) => action(cancellationToken, updatePercent);
        await this.QueueBackgroundWorkItemWithProgressAsync(workItemService, id);
    }

    public async ValueTask QueueBackgroundWorkItemWithProgressAsync(
     Func<CancellationToken, IServiceProvider, Action<decimal, object>, ValueTask> action, string id = null)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        WorkItem workItem = new WorkItem(id ?? Guid.NewGuid().ToString(), action);

        await _queue.Writer.WriteAsync(workItem);
    }

    public async ValueTask<WorkItem> DequeueAsync(CancellationToken cancellationToken) => await _queue.Reader.ReadAsync(cancellationToken);

    public IAsyncEnumerable<WorkItem> ParallelRead(CancellationToken cancellationToken) => _queue.Reader.ReadAllAsync(cancellationToken);

    public async ValueTask Run(WorkItem workItem, CancellationToken token, IServiceProvider provider)
    {
        this._workingItems.Add(workItem);
        await _job.Clients.All.SendAsync("workingItems", this._workingItems.Select(o => o.Id));

        try
        {
            workItem.PercentUpdated += (obj, e) =>
            {
                _job.Clients.All.SendAsync("progress", new { Id = workItem.Id, Percent = e.Percent, Info = e.Info });
            };

            await workItem.Action(token, provider, workItem.UpdateProgress);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            this._workingItems.Remove(workItem);
            await _job.Clients.All.SendAsync("workingItems", this._workingItems.Select(o => o.Id));
        }
    }

    public IEnumerable<WorkItem> GetWorkItems() => this._workingItems;
}
