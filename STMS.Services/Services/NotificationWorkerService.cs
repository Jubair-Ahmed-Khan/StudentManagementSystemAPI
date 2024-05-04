using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace STMS.Services.Services
{
    public class NotificationWorkerService : BackgroundService
    {
        private readonly ILogger<NotificationWorkerService> _logger;

        public NotificationWorkerService(ILogger<NotificationWorkerService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Hourly Notification Worker Service
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);

                _logger.LogInformation("Student Enrollment Ending Soon..."); //this will be shown after every 1 hour
            }
        }
    }
}
