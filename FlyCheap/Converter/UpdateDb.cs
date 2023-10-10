using FlyCheap.Api_Managers;
using FlyCheap.Converter.Comparators;
using FlyCheap.Db_Context;
using FlyCheap.Enums;
using FlyCheap.Models;
using FlyCheap.Models.AirlinesJson;
using FlyCheap.Models.AirportsJson;
using FlyCheap.Models.CountriesJson;
using FlyCheap.Models.Db;
using FlyCheap.Models.JsonModels;
using FlyCheap.Models.JsonModels.Cityes;
using FlyCheap.Utility_Components;
using Microsoft.EntityFrameworkCore;

//using AirportJson = FlyCheap.Models.AirportsJson.AirportJson;

namespace FlyCheap.Converter;

public class UpdateDb
{
    public void ChangeMethodUpdateCollection(TableCode tableCode, LanguageCode languageCode = LanguageCode.Russian)
    {
        var apiForRequestDb = new ApiForRequestDb();
        switch (tableCode)
        {
            case TableCode.Cities:
                var cities = CreateCitiesDb(apiForRequestDb.GetDataBase<List<CitiesJson>>(tableCode, languageCode));
                if (cities != null)
                {
                    RequestToDb<Cities>(cities, new CitiesDbComparer());
                }

                break;
            case TableCode.Airports:
                var airports =
                    CreateAirportsDb(apiForRequestDb.GetDataBase<List<AirportJson>>(tableCode, languageCode));
                if (airports != null)
                {
                    RequestToDb<Airport>(airports, new AirportDbComparer());
                }

                break;
            case TableCode.Airlines:
                var airlines =
                    CreateAirlinesDb(apiForRequestDb.GetDataBase<List<AirlinesJson>>(tableCode, languageCode));
                if (airlines != null)
                {
                    RequestToDb<Airlines>(airlines, new AirlinesDbComparer());
                }

                break;
            case TableCode.Countries:
                var countries =
                    CreateCountriesDb(apiForRequestDb.GetDataBase<List<CountriesJson>>(tableCode, languageCode));
                if (countries != null)
                {
                    RequestToDb<Countries>(countries, new CountriesDbComparer());
                }

                break;
        }
    }


    private static void RequestToDb<T>(IEnumerable<NamedEntity> request, Comparer comparer) where T : NamedEntity
    {
        using var dbContext = new AirDbContext();

        var differences = request
            .OfType<T>()
            .Except(dbContext.Set<T>(), (IEqualityComparer<T>?)comparer)
            .ToList();

        dbContext.Set<T>().AddRange(differences); // Используем DbSet<T> для добавления
        dbContext.SaveChanges();
    }

    private static List<Countries>? CreateCountriesDb(List<CountriesJson>? countries)
    {
        if (countries != null)
        {
            return countries
                .Select(x => new Countries()
                {
                    code = x.code,
                    name = x.name,
                    currency = x.currency,
                }).ToList();
        }

        return default;
    }

    private static List<Cities>? CreateCitiesDb(List<CitiesJson>? cities)
    {
        if (cities != null)
        {
            return cities
                .Select(x => new Cities()
                {
                    code = x.code,
                    country_code = x.country_code,
                    name = x.name,
                    name_translations = x.name_translations.en,
                    time_zone = x.time_zone,
                    lat = x.coordinates.lat,
                    lon = x.coordinates.lon,
                }).ToList();
        }

        return default;
    }

    private static List<Airport>? CreateAirportsDb(List<AirportJson> airports)
    {
        return airports?.Select(x => new Airport
            {
                Code = x.code,
                CityCode = x.city_code,
                country_code = x.country_code,
                name = x.name,
                NameTranslationsEn = x.name_translations.en,
                TimeZone = x.time_zone,
                IataType = x.iata_type,
                Lat = x.coordinates.lat,
                Lon = x.coordinates.lon,
                Flightable = x.flightable,
            })
            // .Where(x => x.IataType == "airport")
            .ToList();
    }

    private static List<Airlines>? CreateAirlinesDb(List<AirlinesJson>? airlines)
    {
        if (airlines != null)
        {
            return airlines
                .Select(x => new Airlines()
                {
                    code = x.code,
                    name = x.name,
                    name_translations = x.name_translations.en,
                    is_lowcost = x.is_lowcost,
                }).ToList();
        }

        return default;
    }
}