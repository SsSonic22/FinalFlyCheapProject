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
using FlyCheapBot;
using FlyCheapBot.Collections;
using FlyCheapBot.State.Models;
using FlyCheapBot.UI;

var botClient = new TelegramBotClient(Configuration.Token);

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { },
    ThrowPendingUpdates = true
};

// Прослушка работы бота, бот постоянно ожидает сообщения от пользователя
botClient.StartReceiving(
    HandleUpdatesAsync,
    Exceptions.HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
await Task.Delay(Int32.MaxValue);

cts.Cancel();

//Метод для обработки обновлений бота (здесь бот прослушивает сообщения {текстовые/c inline клавиатуры)
//Заполнение каталога городами при страрте бота
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
        await botClient.SendTextMessageAsync(tgId, "Выберите действие:", replyMarkup: MainMenu.GetMainMenu());
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
                    $"Ваш город отправления {cityFromMessage}, теперь введите город назначения:");
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

                await botClient.SendTextMessageAsync(tgId, $"Город прибытия {cityFromMessage}, " +
                                                           "теперь введите дату вылета в формате  дд.мм.гггг");
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

                //  Передаем в WorkPlayLoad задачу
                //WorkPayload workPayload = new WorkPayload() { Data = "Задача " + flight.Id.ToString() };
                var result = GetFinalTickets(flight);


                await botClient.SendTextMessageAsync(tgId, "Результат поиска:\n" + result);
                await botClient.SendTextMessageAsync(tgId, "Выберите действие:", replyMarkup: MainMenu.GetMainMenu());
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(tgId,
                    "Дата вылета введена неверно - повторите ввод:в формате  дд.мм.гггг");
            }
        }
    }

//дефолтный ответ бота в случае неправильного ввода команды пользователем
    await botClient.SendTextMessageAsync(tgId, $"Для начала работы с ботом FlyCheap, введите команду /start \n");
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
            $"Введите город вылета"
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
            $"Мои авиарейсы"
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
    //Передаем задачу и запускаем Worker
    // WorkManager.AddWork(workPayload);
    // WorkManager.StartWorker();

    var sb = new StringBuilder();
    var apiAviaSales = new ApiAviaSales();
    var airways = apiAviaSales.FlightSearchRequestCreating(fly.DepartureDate,
        fly.DepartureСity, fly.ArrivalСity);

    if (airways.data.Count != 0)
    {
        foreach (var flightData in airways.data)
        {
            sb.Append("Аэропорт отправления: " + flightData.origin_airport + "\n");
            sb.Append("Аэропорт назначения: " + flightData.destination_airport + "\n");
            sb.Append("Время отправления: " + flightData.departure_at + "\n");
            sb.Append("Авиакомпания: " + flightData.airline + "\n");
            sb.Append("Цена: " + flightData.price + " " + airways.currency + "\n");
            sb.Append("Продолжительность полёта: " + flightData.duration + " Мин." + "\n");
            sb.Append("Номер рейса: " + flightData.flight_number + "\n");
            sb.Append("----------------------------------------------" + "\n");
        }

        return sb.ToString();
    }

    else
    {
        return "Авиарейсов по данному направлению не найдено!";
    }
}