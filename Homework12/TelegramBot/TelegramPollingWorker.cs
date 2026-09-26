using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace TelegramBot;

public class TelegramPollingWorker : BackgroundService
{
    private readonly ITelegramBotClient _botClient;

    public TelegramPollingWorker(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandleErrorAsync,
            cancellationToken: stoppingToken
        );

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message?.Text == null)
        {
            return;
        }

        long chatId = update.Message.Chat.Id;
        string text = update.Message.Text.Trim();

        if (text == "/start")
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Hello! Send me your name.",
                cancellationToken: cancellationToken
            );

            return;
        }

        await botClient.SendMessage(
            chatId: chatId,
            text: $"Hello, {text}!",
            cancellationToken: cancellationToken
        );
    }

    private Task HandleErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        
        Console.WriteLine(exception.Message);
        
        return Task.CompletedTask;
    }
}