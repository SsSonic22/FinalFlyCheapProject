using FlyCheap.Api_Managers;
using FlyCheap.Converter;
using FlyCheap.Enums;
using FlyCheap.Models.AirportsJson;
using FlyCheap.Models.CountriesJson;
using FlyCheap.Models;
using FlyCheap.Models.AirlinesJson;
using FlyCheap.Models.JsonModels.Cityes;
using FlyCheap.Utility_Components;

namespace FlyCheap;

static class Program
{
    private static ApiForRequestDb _apiForRequestDb = new();
    private static UpdateDb _converter = new();
    private static ApiAviaSales _apiAviaSales = new();

    static void Main()
    {
        Console.WriteLine("Start");
        Console.WriteLine(_apiAviaSales.Test());
       // _converter.ChangeMethodUpdateCollection(TableCode.Airports, LanguageCode.Russian);
       // _converter.ChangeMethodUpdateCollection(TableCode.Cities, LanguageCode.Russian);
       // _converter.ChangeMethodUpdateCollection(TableCode.Airlines, LanguageCode.Russian);
       // _converter.ChangeMethodUpdateCollection(TableCode.Countries, LanguageCode.Russian);

        Console.WriteLine("Stop");
    }
}