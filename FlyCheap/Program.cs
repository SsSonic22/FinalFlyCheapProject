using FlyCheap.Api_Managers;
using FlyCheap.Models.AirportsJson;
using FlyCheap.Models.CountriesJson;
using FlyCheap.Models;
using FlyCheap.Utility_Components;

namespace FlyCheap;

static class Program
{
    private static ApiForRequestDb _apiForRequestDb = new();

    static void Main()
    {
        Console.WriteLine("Start");

        var airportJson = _apiForRequestDb.GetDataBase<List<AirportJson>>(ApiForRequestDb.Airports, ApiForRequestDb.Russian);
        var countriesJson = _apiForRequestDb.GetDataBase<List<CountriesJson>>(ApiForRequestDb.Countries, LanguageCodes.LanguageMappings["Russian"]);
        var planesJson = _apiForRequestDb.GetDataBase<List<PlanesJson>>(ApiForRequestDb.Planes, LanguageCodes.LanguageMappings["Russian"]);
        var routesJson = _apiForRequestDb.GetDataBase<List<RoutesJson>>(ApiForRequestDb.Routes, LanguageCodes.LanguageMappings["Russian"]);
        
        Console.WriteLine(1234);
    }
}