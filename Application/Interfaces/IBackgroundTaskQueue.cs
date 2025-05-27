using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IBackgroundTaskQueue
{
    ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem, string id = null);
    ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, IServiceProvider, ValueTask> workItem, string id = null);

    ValueTask QueueBackgroundWorkItemWithProgressAsync(Func<CancellationToken, Action<decimal, object>, ValueTask> workItem, string id = null);
    ValueTask QueueBackgroundWorkItemWithProgressAsync(Func<CancellationToken, IServiceProvider, Action<decimal, object>, ValueTask> workItem, string id = null);

    ValueTask<WorkItem> DequeueAsync(CancellationToken cancellationToken);
    IAsyncEnumerable<WorkItem> ParallelRead(CancellationToken cancellationToken);

    ValueTask Run(WorkItem workItem, CancellationToken token, IServiceProvider provider);

    IEnumerable<WorkItem> GetWorkItems();

}
