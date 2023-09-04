namespace Test_api.ApiManagers;

public class ApiAviaSales
{
    private string apiToken = "bbaadb1a591224a0562c138a0a040e4e";

    // Параметры запроса
    public string currency = "RUB"; // Валюта цен (по умолчанию - RUB)
    public string origin = "AER"; // Код IATA города или аэропорта отправления
    public string destination = "SVO"; // Код IATA города или аэропорта назначения
    public string departureDate = "2023-09"; // Дата отправления (YYYY-MM или YYYY-MM-DD)
    public string returnDate = "2023-10"; // Дата возврата (не указывайте для билетов в один конец)
    public string directFlight = "false"; // Билеты без пересадок (true или false, по умолчанию: false)
    public string market = "ru"; // Рынок источника данных (по умолчанию ru)
    public int limit = 30; // Количество записей на странице (по умолчанию 30, макс. 1000)
    public int page = 1; // Номер страницы (для пропуска результатов)
    public string sorting = "price"; // Сортировка цен (по цене или по популярности маршрута, по умолчанию: по цене)
    public string unique = "false"; // Возвращает только уникальные маршруты (если указан только origin, true или false, по умолчанию: false)

    // URL для запроса
    private string _url = $"https://api.travelpayouts.com/aviasales/v3/prices_for_dates?" +
                          $"origin={origin}&destination={destination}&departure_at={departureDate}" +
                          $"&return_at={returnDate}&unique={unique}&sorting={sorting}&direct={directFlight}" +
                          $"&cy={currency}&limit={limit}&page={page}&one_way=true&token={apiToken}";
    
   public void InputCities(string? departureCity, string destinationCity)
   {
       using (AirportContext airPorts = new())
       {
           var query = airPorts.Airports
               .FirstOrDefault(x => x
                   .NameRus == departureCity)
               ?.IataCode.ToString();


           if (query.Any())
           {
               Console.WriteLine("Результаты:");
           }
           else
           {
               Console.WriteLine("По вашему запросу ничего не найдено.");
           }
       }
   }

   public string? QueryIata(string city)
    {
        using (AirportContext airPorts = new())
        {
            var IataCode = airPorts.Airports
                .FirstOrDefault(x => x
                    .NameRus == city.Trim())
                ?.IataCode.ToString();

            Console.WriteLine("IataCode: " + IataCode);

            if (IataCode.Length == 3)
            {
                Console.WriteLine("Результаты:");
                return IataCode;
            }
            else
            {
                Console.WriteLine("По вашему запросу ничего не найдено.");
            }

            return null;
        }
    }
}