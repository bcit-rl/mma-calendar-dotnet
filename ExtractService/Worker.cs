using ExtractService.Models;

namespace ExtractService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly DBFiller _dbFiller = new DBFiller();
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            
            _dbFiller.run();

            await Task.Delay(60000, stoppingToken);
        }
    }
}
