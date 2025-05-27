using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.Hubs;

public class JobHub : Hub
{
    private readonly IBackgroundTaskQueue _queue;
    public JobHub(IBackgroundTaskQueue queue) => _queue = queue;

    public async Task WorkingItems() => await Clients.All.SendAsync("workingItems", _queue.GetWorkItems().Select(o => o.Id));
}
