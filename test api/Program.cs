using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Test_api.ApiManagers;

namespace Test_api;

class Program
{
    

    static async Task Main()
    {
        // ApiAviaSales _apiAviaSales = new ApiAviaSales();
        Airways? airways = new Airways();
        
        var iata = _apiAviaSales.QueryIata(Console.ReadLine());
        
        
    }
}

/*

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