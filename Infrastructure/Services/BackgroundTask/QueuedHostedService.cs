using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services.BackgroundTask;

public class QueuedHostedService : BackgroundService
{
    private readonly ILogger<QueuedHostedService> _logger;
    private readonly IConfiguration _configuration;
    public QueuedHostedService(IBackgroundTaskQueue taskQueue, IServiceProvider services, IConfiguration configuration, ILogger<QueuedHostedService> logger)
    {
        Services = services;
        TaskQueue = taskQueue;
        _configuration = configuration;
        _logger = logger;
    }

    public IBackgroundTaskQueue TaskQueue { get; }
    public IServiceProvider Services { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            $"Queued Hosted Service is running.{Environment.NewLine}" +
            $"background queue.{Environment.NewLine}");

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItems = TaskQueue.ParallelRead(stoppingToken);

            await Parallel.ForEachAsync(workItems, new ParallelOptions { MaxDegreeOfParallelism = _configuration.GetValue<int>("MaxParallelTask") }, async (workItem, stoppingToken) =>
            {
                try
                {
                    await TaskQueue.Run(workItem, stoppingToken, Services);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            });
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Queued Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}
