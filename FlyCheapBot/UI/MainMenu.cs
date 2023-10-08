// Главное меню телеграм бота

using Telegram.Bot.Types.ReplyMarkups;

namespace FlyCheapBot.FlyCheap.UI;

public static class MainMenu
{
    public static IReplyMarkup? GetMainMenu()
    {
        InlineKeyboardMarkup mainMenu = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Search Flight", "searchFlight"),
                InlineKeyboardButton.WithCallbackData("My Flights", "myFlights"),
            }
        });
        return mainMenu;
    }
}