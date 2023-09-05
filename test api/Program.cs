using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Test_api.ApiManagers;

namespace Test_api;

class Program
{
    static async Task Main()
    {
        StringBuilder sb = new StringBuilder();
        ApiAviaSales _apiAviaSales = new ApiAviaSales();

        Console.WriteLine("город отправления: ");
        var start = Console.ReadLine();
        _apiAviaSales.FindAnAirports(start);
        /*
        Console.WriteLine("город прибытия: ");
        var fininale = Console.ReadLine();
        Console.WriteLine("дата отправления: ");
        var dataStart = Console.ReadLine();
        Console.WriteLine("дата возврата: ");
        var dataReturn = Console.ReadLine();

        var data = _apiAviaSales.FlightSearch(dataStart, dataReturn, start, fininale);
        Console.WriteLine(data.success);
        var route = data.data.ToList();

        foreach (var data1 in route)
        {
            sb.Append("\n Пункт отправления: " + data1.origin_airport);
            sb.Append("\n Пункт назначения: " + data1.destination_airport);
            sb.Append("\n Время отправления: " + data1.departure_at);
            sb.Append("\n Время прибытия: " + data1.return_at);
            sb.Append("\n Цена: " + data1.price);
            sb.Append("\n -------------------------------");
        }

        Console.WriteLine(sb.ToString());
        */
    }
}

/*

        var iata = _apiAviaSales.QueryIata(Console.ReadLine());
        Console.WriteLine("iata: " + iata);



        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    // Далее можете обрабатывать данные из ответа (content)
                    //Console.WriteLine(content);
                    airways = JsonConvert.DeserializeObject<Airways>(content);
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




Console.WriteLine("airways.success ==> " + airways.success);
var airwaysList = airways.data;
Console.WriteLine("airwaysList.Count ==> " + airwaysList.Count);
for (int i = 0; i < airwaysList.Count; i++)
{
    Console.WriteLine("итерация " + i);
    Console.WriteLine(airways.data[i].airline);
    //Console.WriteLine(airways.data[i].link);
}





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