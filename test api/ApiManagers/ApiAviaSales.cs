using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Test_api.ApiManagers;

public class ApiAviaSales
{
    private string apiToken = "bbaadb1a591224a0562c138a0a040e4e";

    // Параметры запроса
    string currency = "RUB"; // Валюта цен (по умолчанию - RUB)

    // string origin = "AER"; // Код IATA города или аэропорта отправления
    //string destination = "SVO"; // Код IATA города или аэропорта назначения
    //string departureDate = "2023-09"; // Дата отправления (YYYY-MM или YYYY-MM-DD)
    //string returnDate = "2023-10"; // Дата возврата (не указывайте для билетов в один конец)
    string directFlight = "false"; // Билеты без пересадок (true или false, по умолчанию: false)
    string market = "us"; // Рынок источника данных (по умолчанию ru)
    int limit = 30; // Количество записей на странице (по умолчанию 30, макс. 1000)
    int page = 1; // Номер страницы (для пропуска результатов)
    string sorting = "price"; // Сортировка цен (по цене или по популярности маршрута, по умолчанию: по цене)

    string
        unique = "false"; // Возвращает только уникальные маршруты (если указан только origin, true или false, по умолчанию: false)

    public Airways? FlightSearch(string departureDate, string? returnDate, string departureCity = "none",
        string destinationCity = "none")
    {
        string ReturnDateConstructor = $"return_at={returnDate}&";
        if (returnDate == null)
        {
            ReturnDateConstructor = "";
        }

        string origin = QueryIata(departureCity);
        string destination = QueryIata(destinationCity);

        string _url = $"https://api.travelpayouts.com/aviasales/v3/prices_for_dates?" +
                      $"origin={origin}&destination={destination}&departure_at={departureDate}" +
                      $"&{ReturnDateConstructor}unique={unique}&sorting={sorting}&direct={directFlight}" +
                      $"&cy={currency}&limit={limit}&page={page}&one_way=true&token={apiToken}";
        
        Airways? airways = new Airways();
        
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var response = client.GetAsync(_url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
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

        return airways;
    }


    public string QueryIata(string city)
    {
        using (AirportContext airPorts = new())
        {
            var IataCode = airPorts.Airports
                .FirstOrDefault(x => x
                    .NameRus == city.Trim())
                ?.IataCode.ToString();

            if (IataCode != null && IataCode.Any())
            {
                Console.WriteLine("Результат IataCode: " + IataCode);
                return IataCode;
            }

            var IataCodeFromEng = airPorts.Airports
                .FirstOrDefault(x => x
                    .NameEng == city.Trim())
                ?.IataCode.ToString();

            if (IataCodeFromEng != null)
            {
                Console.WriteLine("Результат IataCodeFromEng: " + IataCodeFromEng);
                return IataCodeFromEng;
            }
            else
            {
                Console.WriteLine("По вашему запросу ничего не найдено.");
            }

            return "none";
        }
    }
}