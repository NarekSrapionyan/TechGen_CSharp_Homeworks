using Telegram.Bot;
namespace TelegramBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
    
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi&#xA;
        // builder.Services.AddOpenApi();

        var botToken = builder.Configuration["Telegram:BotToken"] ?? throw new InvalidOperationException("Telegram BotToken is not configured.");        
        builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));
        builder.Services.AddHostedService<TelegramPollingWorker>();
        var app = builder.Build();
        
        
        
        // Configure the HTTP request pipeline.&#xA;
        /* if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        */
        // app.UseHttpsRedirection();
        // app.UseAuthorization();
        
        
        app.Run();
    }
}