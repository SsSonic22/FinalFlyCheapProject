using FlyCheap.Models;
using FlyCheap.Utility_Components;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FlyCheap.Models;
using Newtonsoft.Json;

namespace FlyCheap.Api_Managers;

public class ApiForRequestDb
{
    private const string BaseUrl = "http://api.travelpayouts.com";
    public static string Russian => "ru/";
    public static string English => "en/";
    public static string Airports => "airports.json";
    public static string Countries => "countries.json";
    public static string Cities => "cities.json";
    public static string Airlines => "airlines.json";
    public static string Alliances => "alliances.json";
    public static string Planes => "planes.json"; //Данные в базе более не обновляются
    public static string Routes => "routes.json"; //Данные в базе более не обновляются

    /// <summary>
    /// Получение баз данных. 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="language"></param>
    /// <typeparam name="TOutput"></typeparam> Всегда List! 
    /// <returns></returns>
    public TOutput? GetDataBase<TOutput>(string file, string language = "ru/") where TOutput : class
    {
        if (file is "planes.json" or "routes.json") language = "";

        var RequestFromDataBase = new HttpRequestForRequestFromDataBase()
        {
            file = file,
            language = language,
        };
        var response = HttpRequest(RequestFromDataBase);
        if (response.Ok) return ConvertJsonToDb<TOutput>(response.content);
        Console.WriteLine("response.content ==> " + response.content);
        return null;
    }

    private ResponseContainer HttpRequest(HttpRequestForRequestFromDataBase httpRequest)
    {
        var url = $"{BaseUrl}/data/{httpRequest.language}{httpRequest.file}";
        var responseContainer = new ResponseContainer();

        using (var client = new HttpClient())
        {
            try
            {
                var response = client.GetAsync(url).Result;
                string content = "";
                Console.WriteLine(content);

                if (response.IsSuccessStatusCode)
                {
                    responseContainer.Ok = true;
                    responseContainer.content = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine("content ===> " + content);
                    return responseContainer;
                }
                else
                {
                    Console.WriteLine("Ошибка запроса");
                    responseContainer.content = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine("content ===> " + content);
                    //throw new InvalidOperationException("Ошибка запроса!");
                    return responseContainer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка подключения: {ex.Message}");
                return responseContainer;
            }
        }
    }

    private TOutput ConvertJsonToDb<TOutput>(string input)
    {
        return JsonConvert.DeserializeObject<TOutput>(input);
    }
}


/*

 public class ApiForRequestDb
    {
        private const string BaseUrl = "http://api.travelpayouts.com";
        public static string Russian => "ru/";
        public static string English => "en/";
        public static string Airports => "airports.json";
        public static string Countries => "countries.json";
        public static string Cities => "cities.json";
        public static string Airlines => "airlines.json";
        public static string Alliances => "alliances.json";
        public static string Planes => "planes.json"; //Данные в базе более не обновляются
        public static string Routes => "routes.json"; //Данные в базе более не обновляются

        public async Task<TOutput?> GetDataBase<TOutput>(string file, string language) where TOutput : class
        {
            if (file is "planes.json" or "routes.json") language = "";

            var RequestFromDataBase = new HttpRequestForRequestFromDataBase()
            {
                file = file,
                language = language,
            };

            var response = await HttpRequest(RequestFromDataBase);

            if (response.Ok)
            {
                return ConvertJsonToDb<TOutput>(response.content);
            }

            Console.WriteLine("response.content ==> " + response.content);
            return null;
        }

        private async Task<ResponseContainer> HttpRequest(HttpRequestForRequestFromDataBase httpRequest)
        {
            var url = $"{BaseUrl}/data/{httpRequest.language}{httpRequest.file}";
            var responseContainer = new ResponseContainer();

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);

                    if (response.IsSuccessStatusCode)
                    {
                        responseContainer.Ok = true;
                        responseContainer.content = content;
                        return responseContainer;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка запроса");
                        responseContainer.content = content;
                        return responseContainer;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка подключения: {ex.Message}");
                    return responseContainer;
                }
            }
        }

        private TOutput ConvertJsonToDb<TOutput>(string input)
        {
            return JsonConvert.DeserializeObject<TOutput>(input);
        }
    }
}
*/