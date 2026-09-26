namespace TelegramBot;

public class TelegramPollingWorker : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}