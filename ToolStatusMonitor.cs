using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TSMonitor2
{
    public class ToolStatusMonitor : BackgroundService
    {
        private readonly ILogger<ToolStatusMonitor> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public ToolStatusMonitor(ILogger<ToolStatusMonitor> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Add your tool status monitoring logic here.
                _logger.LogInformation("ToolStatusMonitor is running at: {time}", DateTimeOffset.Now);

                // Check the tool statuses and send messages as needed.
                // You can use dependency injection to access services.

                // Sleep for a specific interval (e.g., every 15 minutes).
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }
}
