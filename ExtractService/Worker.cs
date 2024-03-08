using ExtractService.Models;

namespace ExtractService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private DBFiller? _dbFiller = null;
    private readonly IServiceProvider _serviceProvider;
    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<CardContext>();

            if (_dbFiller == null && context != null)
            {
                _dbFiller = new DBFiller(context);

                await _dbFiller.run();
                context.Dispose();
                await Task.Delay(864000000, stoppingToken);
            }
        }
    }
}
