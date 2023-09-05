using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Test_api.ApiManagers;
 public class ApiAviaSales
 {
     private string apiToken = "bbaadb1a591224a0562c138a0a040e4e";
 
     // Параметры запроса
     string currency = "RUB"; // Валюта цен (по умолчанию - RUB)
 
     //string origin = "AER"; // Код IATA города или аэропорта отправления
     //string destination = "SVO"; // Код IATA города или аэропорта назначения
     //string departureDate = "2023-09"; // Дата отправления (YYYY-MM или YYYY-MM-DD)
     //string returnDate = "2023-10"; // Дата возврата (не указывайте для билетов в один конец)
     string directFlight = "false"; // Билеты без пересадок (true или false, по умолчанию: false)
     string market = "ru"; // Рынок источника данных (по умолчанию ru)
     int limit = 30; // Количество записей на странице (по умолчанию 30, макс. 1000)
     int page = 1; // Номер страницы (для пропуска результатов)
     string sorting = "price"; // Сортировка цен (по цене или по популярности маршрута, по умолчанию: по цене)
 
     string
         unique = "false"; // Возвращает только уникальные маршруты (если указан только origin, true или false, по умолчанию: false)
 
     public Airways FlightSearchRequestCreating(string departureDate, string? returnDate = null,
         string departureCity = "none",
         string destinationCity = "none")
     {
         var departureAirPorts = FindAnAirports(departureCity);
         var destinationAirPorts = FindAnAirports(destinationCity);
         var flightData = new ObjectForRequestFlight()
         {
             departureAirPorts = departureAirPorts,
             destinationAirPorts = destinationAirPorts,
             returnDate = returnDate,
             departureDate = departureDate,
         };
 
         return RequestFlight(flightData);
     }
 
     private Airways RequestFlight(ObjectForRequestFlight flightData)
     {
         Airways airways = new Airways
         {
             currency = "",
             success = false,
             data = new List<Data>()
         };
 
         var httpRequest = new ObjectForHttpRequest();
 
         foreach (var airportStart in flightData.departureAirPorts)
         {
             httpRequest.origin = airportStart.IataCode;
             foreach (var airportFinal in flightData.destinationAirPorts)
             {
                 httpRequest.destination = airportFinal.IataCode;
                 httpRequest.departureDate = flightData.departureDate;
                 httpRequest.returnDate = flightData.returnDate;
                 Thread.Sleep(100);
 
                 try
                 {
                     string content = HttpRequest(httpRequest);
                     if (!string.IsNullOrEmpty(content))
                     {
                         Airways airways1 = JsonConvert.DeserializeObject<Airways>(content);
                         if (airways1.success)
                         {
                             airways.data.AddRange(airways1.data);
                             airways.success = true;
                             airways.currency = airways1.currency;
                         }
                     }
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("Произошла ошибка при парсинге JSON: " + e.Message);
                 }
             }
         }
 
         return airways;
     }
     
     private string? HttpRequest(ObjectForHttpRequest httpRequest)
     {
         string returnDateConstructor =
             string.IsNullOrEmpty(httpRequest.returnDate) ? "" : $"return_at={httpRequest.returnDate}&";
 
         string url = $"https://api.travelpayouts.com/aviasales/v3/prices_for_dates?" +
                      $"origin={httpRequest.origin}&destination={httpRequest.destination}&departure_at={httpRequest.departureDate}" +
                      $"&{returnDateConstructor}unique={unique}&sorting={sorting}&direct={directFlight}" +
                      $"&cy={currency}&limit={limit}&page={page}&one_way=true&token={apiToken}";
 
         //Console.WriteLine(url);
         using (HttpClient client = new HttpClient())
         {
             try
             {
                 var response = client.GetAsync(url).Result;
                 string content = "";
                 Console.WriteLine(content);
 
                 if (response.IsSuccessStatusCode)
                 {
                     content = response.Content.ReadAsStringAsync().Result;
                     //Console.WriteLine("content ===> " + content);
                     return content;
                 }
                 else
                 {
                     Console.WriteLine("Ошибка запроса");
                     content = response.Content.ReadAsStringAsync().Result;
                     //Console.WriteLine("content ===> " + content);
                     //throw new InvalidOperationException("Ошибка запроса!");
                     return content;
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Произошла ошибка: {ex.Message}");
                 return null;
             }
         }
 
         return null;
     }
 
     private List<AirportDb>? FindAnAirports(string city = "")
     {
         using (AirportContext airPorts = new())
         {
             var airports = airPorts.Airports
                 .Where(x => x
                     .NameRus.Contains(city.Trim()) || x
                     .NameEng.Contains(city.Trim()) || x
                     .CityRus.Contains(city.Trim()) || x
                     .CityEng.Contains(city.Trim())).ToList();
 
             if (airports.Count > 10)
             {
                 Console.WriteLine("Уточтите критерии поиска!");
                 return null;
             }
 
             if (airports.Any())
             {
                 /*
                 StringBuilder sb = new StringBuilder();
 
                 foreach (var airportDb in airports)
                 {
                     sb.Append("\n Результат IataCode : " + airportDb.IataCode);
                     sb.Append("\n Аеропорт: " + airportDb.NameEng);
                     sb.Append("\n Город: " + airportDb.CityEng);
                     sb.Append("\n -------------------------------");
                 }
 
                 Console.WriteLine(sb.ToString());
               //  */
                 return airports;
             }
 
             Console.WriteLine("По вашему запросу ничего не найдено.");
             return airports;
         }
     }
 }
