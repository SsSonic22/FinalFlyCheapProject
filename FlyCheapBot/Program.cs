//Telegram bot FlyCheap

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;
using System.Text;
using FlyCheap;
using FlyCheap.Api_Managers;
using FlyCheapBot.FlyCheap;
using FlyCheapBot.FlyCheap.Collections;
using FlyCheapBot.FlyCheap.State.Models;
using FlyCheapBot.FlyCheap.UI;

#region

var botClient = new TelegramBotClient(Configuration.Token);

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { },
    ThrowPendingUpdates = true
};

// Прослушка работы бота
botClient.StartReceiving(
    HandleUpdatesAsync,
    Exceptions.HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
await Task.Delay(Int32.MaxValue);

cts.Cancel();

#endregion

//Метод для обработки обновлений бота
async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (CitiesCollection.cities.Count == 0)
    {
        CitiesCollection.cities = Catalogs.GetCities();
    }

    if (update.Type == UpdateType.Message && update?.Message?.Text != null)
    {
        await HandleCommandMessage(botClient, update.Message);
        return;
    }

    if (update.Type == UpdateType.CallbackQuery)
    {
        await HandleCallbackQuery(botClient, update.CallbackQuery);
        return;
    }
}

//Метод ожидающий от пользователем ввода сообщения и обрабатывающий их
async Task HandleCommandMessage(ITelegramBotClient botClient, Message message)
{
    var tgId = message.From.Id;
    var user = UserUtils.GetOrCreate(tgId);
    var text = message.Text.ToLower();

    if (text == "/start")
    {
        await botClient.SendTextMessageAsync(tgId, "Choose Button:", replyMarkup: MainMenu.GetMainMenu());
        return;
    }

    if (user.InputState != InputState.Nothing)
    {
        //Парсим город вылета ----->>>>>>>>>>>>>>>>>>>>>>>>
        if (user.InputState == InputState.DepartureСity)
        {
            var cityFromMessage = message.Text
                //.ToLower()
                ;
            var selectedCityFromList = CitiesCollection.cities
                .FirstOrDefault(x => x.Contains(cityFromMessage));

            if (selectedCityFromList != null)
            {
                var flight = FlightsList.flights
                    .First(x => x.UserTgId == tgId && x.resultTickets == null);

                flight.DepartureСity = cityFromMessage;

                await botClient.SendTextMessageAsync(tgId,
                    $"ваш город отправления {cityFromMessage}, теперь введите город назначения:");
                user.InputState = InputState.ArrivalСity;
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(tgId, "Город отправления не найден - повторите ввод:");
            }
        }

        //Парсим город прибытия ----->>>>>>>>>>>>>>>>>>>>>>>>
        if (user.InputState == InputState.ArrivalСity)
        {
            var cityFromMessage = message.Text
                //.ToLower()
                ;
            var selectedCityFromList = CitiesCollection.cities
                .First(x => x.Contains(cityFromMessage));

            if (selectedCityFromList != null)
            {
                var flight = FlightsList.flights
                    .First(x => x.UserTgId == tgId && x.resultTickets == null);

                flight.ArrivalСity = cityFromMessage;

                await botClient.SendTextMessageAsync(tgId, $"город прибытия {cityFromMessage}, " +
                                                           "теперь введите дату отправления в формате  дд.мм.гггг");
                user.InputState = InputState.DepartureDate;
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(tgId, "Город прибытия не найден - повторите ввод:");
            }
        }

        //Парсим дату вылета ----->>>>>>>>>>>>>>>>>>>>>>>>
        if (user.InputState == InputState.DepartureDate)
        {
            var dateFromMessage = message.Text;
            DateTime parsedDate;
            if (DateTime.TryParse(dateFromMessage, out parsedDate))
            {
                var flight = FlightsList.flights
                    .First(x => x.UserTgId == tgId && x.resultTickets == null);

                flight.DepartureDate = parsedDate;

                user.InputState = InputState.FullState;

                var result = GetFinalTickets(flight);

                await botClient.SendTextMessageAsync(tgId, "Результат поиска:\n" + result);
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(tgId,
                    "дата вылета введена неверно - повторите ввод:в формате  дд.мм.гггг");
            }
        }
    }

//дефолтный ответ бота в случае неправильного ввода команды пользователем
    await botClient.SendTextMessageAsync(tgId, $"To start working with the bot, send the command /start \n");
}

//Метод обрабатывающий нажатие определенной кнопки inline клавиатуры
async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    var tgId = callbackQuery.From.Id;
    var user = UserUtils.GetOrCreate(tgId);

    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

    //Поиск авиабилетов ----->>>>>>>>>>>>>>>>>>>>>>>>
    if (callbackQuery.Data.StartsWith("searchFlight"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Enter the city of departure"
        );

        user.InputState = InputState.DepartureСity;
        var fly = new Fly(tgId);
        FlightsList.flights.Add(fly);
        return;
    }

    //Мои рейсы/избранное ----->>>>>>>>>>>>>>>>>>>>>>
    if (callbackQuery.Data.StartsWith("myFlights"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"You choose My Flights"
        );
        return;
    }

    //Перезапуск бота ----->>>>>>>>>>>>>>>>>>>>>>>>>
    if (callbackQuery.Data.StartsWith("restartBot"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"You choose Restart"
        );
        return;
    }

    await botClient.SendTextMessageAsync(
        callbackQuery.Message.Chat.Id,
        $"You choose with data: {callbackQuery.Data}"
    );
    return;
}

//Метод вывода найденных результатов по авиарейсам
string GetFinalTickets(Fly fly)
{
    var sb = new StringBuilder();
    var apiAviaSales = new ApiAviaSales();
    var humanReadableConverter = new HumanReadableConverter();
    var airports =
        humanReadableConverter.GetHumanReadableAirways(apiAviaSales.FlightSearchRequestCreating(fly.DepartureDate,
            fly.DepartureСity, fly.ArrivalСity));

    foreach (var flightData in airports.data)
    {
        sb.Append("Аэропорт отправления: " + flightData.origin_airport + "\n");
        sb.Append("Аэропорт назначения: " + flightData.destination_airport + "\n");
        sb.Append("Время отправления: " + flightData.departure_at + "\n");
        sb.Append("Авиакомпания: " + flightData.airline + "\n");
        sb.Append("Цена: " + flightData.price + " " + airports.currency + "\n");
        sb.Append("Продолжительность полёта: " + flightData.duration + "\n");
        sb.Append("Номер рейса: " + flightData.flight_number + "\n");
        sb.Append("----------------------------------------------" + "\n");
    }

    Console.WriteLine(sb.ToString());
    return sb.ToString();
}