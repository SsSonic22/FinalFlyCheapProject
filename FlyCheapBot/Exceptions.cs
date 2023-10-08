using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace FlyCheapBot.FlyCheap;

public static class Exceptions
{
    //Метод для обработки ошибок (бота или API телеграмма)
    public static Task HandleErrorAsync(ITelegramBotClient client, Exception exception,
        CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API error:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}