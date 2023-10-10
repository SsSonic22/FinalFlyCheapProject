// Главное меню телеграм бота

using Telegram.Bot.Types.ReplyMarkups;

namespace FlyCheapBot.UI;

public static class MainMenu
{
    public static IReplyMarkup? GetMainMenu()
    {
        InlineKeyboardMarkup mainMenu = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Поиск авиарейса", "searchFlight"),
                InlineKeyboardButton.WithCallbackData("Мои рейсы", "myFlights"),
            }
        });
        return mainMenu;
    }
}