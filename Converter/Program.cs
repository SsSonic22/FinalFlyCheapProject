using Converter.Models;
using Newtonsoft.Json;

namespace Converter;

static class Program
{
    // Путь к файлу JSON
    static string filePath = @"D:\Array.json";

    static void Main()
    {
        Console.WriteLine("Start");
        string json = File.ReadAllText(filePath);
        // Console.WriteLine(json);

        List<Airport>? airportsInput = JsonConvert.DeserializeObject<List<Airport>>(json);
        Console.WriteLine("airportsInput.Capacity ==>> " + airportsInput.Capacity);

        using AirportContext airPorts = new();
        for (int i = 0; i < airportsInput.Count; i++)
        {
            var APort = airportsInput[i];
            
            if (airportsInput[i] != null)
            {
                airPorts.Airports.Add(ConverterToAirportDb(APort));
            }
        }

        airPorts.SaveChanges();
    }

    public static AirportDb ConverterToAirportDb(Airport airport)
    {
        AirportDb airportDb = new AirportDb();


        airportDb.IataCode = airport.IataCode;
        airportDb.CityEng = airport.CityEng;
        airportDb.CountryEng = airport.CountryEng;
        airportDb.NameEng = airport.NameEng;
        airportDb.IcaoCode = airport.IcaoCode;
        airportDb.IsoCode = airport.IsoCode;
        airportDb.CityRus = airport.CityRus;
        airportDb.CountryRus = airport.CountryRus;
        airportDb.NameRus = airport.NameRus;
        airportDb.Longitude = airport.Longitude;
        airportDb.GmtOffset = airport.GmtOffset;
        airportDb.Latitude = airport.Latitude;

        return airportDb;
    }
}