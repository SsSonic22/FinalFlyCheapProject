using Converter.Models;
using Converter.Utility_Components;
using Newtonsoft.Json;

namespace Converter;

public class Airports
{
    public void Converter(string filePath)
    {
        string json = File.ReadAllText(filePath);
        var ok = RepeatCheckingAndAddToDb(JsonConvert.DeserializeObject<List<Airport>>(json));
        //var q = JsonConvert.DeserializeObject<List<Airport>>(json);
        if (ok) Console.WriteLine("База данных обновлена!");
       // Console.WriteLine("");
    }

    /// <summary>
    /// Функция проверяющая на наличие в базе данных аналогичных данных и добавляющая записи в случае их отсутствия
    /// </summary>
    /// <param name="aviacompany"></param>
    /// <returns></returns>
    private bool RepeatCheckingAndAddToDb(List<Airport> airportsJson)
    {
        var cleanInputData = CleaningInputData(airportsJson);
        var bufferListAirportDb = new List<AirportDb>();
       
        foreach (var airport in cleanInputData)
        {
            var airportDb = new AirportDb
             {
                IcaoCode = airport.IcaoCode, //?? "none",
                IataCode = airport.IataCode,
                CityEng = airport.CityEng,
                CountryEng = airport.CountryEng,
                NameEng = airport.NameEng,
                IsoCode = airport.IsoCode,
                CityRus = airport.CityRus,
                CountryRus = airport.CountryRus,
                NameRus = airport.NameRus,
                Longitude = airport.Longitude,
                GmtOffset = airport.GmtOffset,
                Latitude = airport.Latitude,
                
            };
             bufferListAirportDb.Add(airportDb);
        }

        using var airports = new AviaInfoContext();
        var airportDbComparer = new AirportDbEqualityComparer();
        var differences = bufferListAirportDb.Except(airports.Airports, airportDbComparer).ToList();

        
            //var differences = bufferListAirportDb.Except(airports.Airports).ToList();
        airports.Airports.AddRange(differences);

        try
        {
            airports.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка записи в базу данных! " + e);
            return false;
        }
    }
    
    private List<Airport> CleaningInputData(List<Airport> airportInput)
    {
        var input = airportInput.Where(x => !string.IsNullOrEmpty(x.IataCode))
            .DistinctBy(x => new {x.IataCode, x.NameEng, x.IcaoCode})
            .ToList();

        return input;
    }
}

/*
var input = airportInput
            .Where(x => !string.IsNullOrEmpty(x.airports.Where(x => x.IataCode)))
            .DistinctBy(x => new {x.iata_code, x.name, x.icao_code})
            .ToList();

        input.Where(x => x.icao_code == null).ToList().ForEach(x => x.icao_code = "none");
*/