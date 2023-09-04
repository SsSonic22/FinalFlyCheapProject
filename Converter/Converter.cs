using Converter.Models;

namespace Converter;

public class Converter
{
    public AirportDb ConverterToAirportDb(Airport airport)
    {
        AirportDb airportDb = null;

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
        // airportDb.Latitude = airport.Latitude;
        return airportDb;
    }
}