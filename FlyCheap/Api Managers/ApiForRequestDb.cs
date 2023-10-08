using FlyCheap.Models;
using FlyCheap.Utility_Components;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FlyCheap.Models;
using Newtonsoft.Json;
using FlyCheap.Enums;

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
    /// <param name="tableCode">Выбор таблицы для обновления </param>
    /// <param name="language">Выбор языка. По умолчанию русский.</param>
    /// <typeparam name="TOutput"></typeparam> Всегда List! 
    /// <returns></returns>
    public TOutput? GetDataBase<TOutput>(TableCode tableCode,
        LanguageCode languageCode = LanguageCode.Russian /*string language = "ru/"*/) where TOutput : IEnumerable<NamedEntity>
    {
        //if (file is "planes.json" or "routes.json") language = "";

        var file = ParametersMap.TableFileMappings.FirstOrDefault(x => x.Key == tableCode).Value;
        var lang = ParametersMap.LanguageMappings.FirstOrDefault(x => x.Key == languageCode).Value;

        if (tableCode is TableCode.Planes or TableCode.Routes)
        {
            lang = ParametersMap.LanguageMappings.FirstOrDefault(x => x.Key == LanguageCode.None).Value;
        }

        var RequestFromDataBase = new HttpRequestForRequestFromDataBase()
        {
            file = file,
            //language = language,
            language = lang,
        };

        var response = HttpRequest(RequestFromDataBase);
        if (response.Ok)
        {
            return ConvertFromJsonToDbFormat<TOutput>(response.content);
        }

        Console.WriteLine("response.content ==> " + response.content);
        return default;
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

                if (response.IsSuccessStatusCode)
                {
                    responseContainer.Ok = true;
                    responseContainer.content = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine("content ===> " + content);
                }
                else
                {
                    Console.WriteLine("Ошибка запроса");
                    responseContainer.Ok = false;
                    responseContainer.content = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine("content ===> " + content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка подключения: {ex.Message}");
            }

            return responseContainer;
        }
    }

    private TOutput? ConvertFromJsonToDbFormat<TOutput>(string input) where TOutput : IEnumerable<NamedEntity>
    {
        var output = JsonConvert.DeserializeObject<TOutput>(input);
        
        foreach (var item in output)
        {
            if (item.name == null)
            {
                item.name = "none"; // Заменяем значение поля name на пустую строку
            }
        }
        
        if (output.Any(x => x.name == null))
        {
            throw new InvalidOperationException("------>>>>Обнаружены объекты с полем name, равным null.");
        }
        // return default;
        return output;
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