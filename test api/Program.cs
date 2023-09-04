using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Test_api;

class Program
{
    static async Task Main()
    {
        // Укажите свой токен API
        string apiToken = "bbaadb1a591224a0562c138a0a040e4e";

        // Параметры запроса
        string currency = "usd"; // Валюта цен (по умолчанию - RUB)
        string origin = "MAD"; // Код IATA города или аэропорта отправления
        string destination = "BCN"; // Код IATA города или аэропорта назначения
        string departureDate = "2023-07"; // Дата отправления (YYYY-MM или YYYY-MM-DD)
        string returnDate = "2023-08"; // Дата возврата (не указывайте для билетов в один конец)
        string directFlight = "false"; // Билеты без пересадок (true или false, по умолчанию: false)
        string market = "us"; // Рынок источника данных (по умолчанию ru)
        int limit = 30; // Количество записей на странице (по умолчанию 30, макс. 1000)
        int page = 1; // Номер страницы (для пропуска результатов)
        string sorting = "price"; // Сортировка цен (по цене или по популярности маршрута, по умолчанию: по цене)
        string unique = "false"; // Возвращает только уникальные маршруты (если указан только origin, true или false, по умолчанию: false)

        // URL для запроса
        string url = $"https://api.travelpayouts.com/aviasales/v3/prices_for_dates?" +
                     $"origin={origin}&destination={destination}&departure_at={departureDate}" +
                     $"&return_at={returnDate}&unique={unique}&sorting={sorting}&direct={directFlight}" +
                     $"&cy={currency}&limit={limit}&page={page}&one_way=true&token={apiToken}";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    // Далее можете обрабатывать данные из ответа (content)
                    Console.WriteLine(content);
                    Airways? airways = JsonConvert.DeserializeObject<Airways>(content);
                    Console.WriteLine(airways.success);
                    int i = 0;
                    foreach (var data in airways.data)
                    {
                        
                        Console.WriteLine("итерация " + i++);
                        Console.WriteLine(data.airline);
                        Console.WriteLine(data.link);
                    }
                }
                else
                {
                    Console.WriteLine("Произошла ошибка при выполнении запроса.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        
    }
}

/*
 Console.Write("Введите код страны (например, RU): ");
        string inputVar = Console.ReadLine().Trim().ToUpper();

        using (AirportContext airPorts = new())
        {
            var query = airPorts.Airports
                .Where(x => x.IsoCode == inputVar)
                .Select(x => new
                {
                    x.IataCode,
                    x.CityRus,
                    x.CountryRus
                })
                .ToList();

            if (query.Any())
            {
                Console.WriteLine("Результаты:");
                foreach (var airport in query)
                {
                    Console.WriteLine(
                        $"IATA Код: {airport.IataCode}, Город: {airport.CityRus}, Страна: {airport.CountryRus}");
                }
            }
            else
            {
                Console.WriteLine("По вашему запросу ничего не найдено.");
            }
        }
 
 
 
 
 using (var context = new AirportContext())
        {
            // Получить все аэропорты
            var airports = context.Airports.ToList();

            // Добавить новый аэропорт

            context.Airports.Add(newAirport);
            context.SaveChanges();

            // Другие операции с данными
        }
*/