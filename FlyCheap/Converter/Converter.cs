using FlyCheap.Models.AirportsJson;
using FlyCheap.Api_Managers;
using FlyCheap.Db_Context;
using FlyCheap.Models;

namespace FlyCheap.Converter;

public class Converter
{
    public void RefreshAirports()
    {
        var apiForRequestDb = new ApiForRequestDb();

        var airportsInput =
            apiForRequestDb.GetDataBase<List<AirportJson>>(ApiForRequestDb.Airports, ApiForRequestDb.Russian);
        //Дописать!
        var airportsBuffer = airportsInput
            .Select(x => new AirportDb
            {
                IataCode = x.code, 
                CityCode = x.city_code,
            })
        using var airports = new AirDbContext();

        airports.Airports.AddRange(differences);
        airports.SaveChanges();
    }
}