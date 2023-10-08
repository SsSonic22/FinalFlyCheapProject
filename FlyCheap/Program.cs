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

/*
 * //Метод вывода найденных результатов по авиарейсам
string GetFinalTickets(Fly fly)
{
    var sb = new StringBuilder();
    var apiAviaSales = new ApiAviaSales();
    var humanReadableConverter = new HumanReadableConverter();
    var airports =
        humanReadableConverter.GetHumanReadableAirways(apiAviaSales.FlightSearchRequestCreating(fly.DepartureDate,
            fly.DepartureСity, fly.ArrivalСity));
    
    foreach (var flightData in airports.data)
    {
        sb.Append("origin_airport: " + flightData.origin_airport + "\n");
        sb.Append("destination_airport: " + flightData.destination_airport + "\n");
        sb.Append("departure_at: " + flightData.departure_at + "\n");
        sb.Append("airline: " + flightData.airline + "\n");
        sb.Append("price: " + flightData.price + "\n");
        sb.Append("----------------------------------------------" + "\n");
    }

    return sb.ToString();
}
*/