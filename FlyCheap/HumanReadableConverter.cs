using System.Reflection.Metadata;
using FlyCheap.Db_Context;
using FlyCheap.Models;
using FlyCheap.Models.JsonModels;
using Microsoft.EntityFrameworkCore;
using HumanReadableAirways = FlyCheap.Models.HumanReadableAirways;

namespace FlyCheap;

public class HumanReadableConverter
{
    public HumanReadableAirways GetHumanReadableAirways(AirwaysJson airwaysJson)
    {
        var humanReadableAirways = new HumanReadableAirways
        {
            currency = airwaysJson.currency,
            data = new List<FlightDataForHumans>()  
        };

        foreach (var flightData in airwaysJson.data)
        {
            var dataItem = new FlightDataForHumans
            {
                origin_airport = GetAirport(flightData.origin_airport),
                destination_airport = GetAirport(flightData.destination_airport),
                airline = GetAirline(flightData.airline),
                origin = GetCities(flightData.origin),
                destination = GetCities(flightData.destination),
                price = flightData.price,
                departure_at = flightData.departure_at,
                return_at = flightData.return_at,
                duration = flightData.duration,
                duration_back = flightData.duration_back,
                duration_to = flightData.duration_to,
                transfers = flightData.transfers,
                return_transfers = flightData.return_transfers,
                flight_number = flightData.flight_number
            };  

            humanReadableAirways.data.Add(dataItem);
        }

        return humanReadableAirways;
    }

    private string GetCities(string citiesCode)
    {
        using (AirDbContext dbContext = new())
        {
            return dbContext.Cityes
                .AsNoTracking()
                .FirstOrDefault(x => x
                    .code == citiesCode)
                .name;
        }
    }

    private string GetAirline(string airlineCode)
    {
        using (AirDbContext dbContext = new())
        {
            return dbContext.Airlines
                .AsNoTracking()
                .FirstOrDefault(x => x
                    .code == airlineCode)
                .name_translations;
        }
    }

    private string GetAirport(string airportCode)
    {
        using (AirDbContext dbContext = new())
        {
            return dbContext.Airports
                .AsNoTracking()
                .FirstOrDefault(x => x
                    .Code == airportCode)
                .name;
        }
    }
}

/*
public HumanReadableAirways GetHumanReadableAirways(AirwaysJson airwaysJson)
    {
        var humanReadableAirways = new HumanReadableAirways
        {
            currency = airwaysJson.currency,
            // data = airwaysJson.data
        };

        for (int i = 0; i < airwaysJson.data.Count; i++)
        {
            humanReadableAirways.data[i].origin_airport = GetAirport(airwaysJson.data[i].origin_airport);
            humanReadableAirways.data[i].destination_airport = GetAirport(airwaysJson.data[i].destination_airport);
            humanReadableAirways.data[i].airline = GetAirline(airwaysJson.data[i].airline);
            humanReadableAirways.data[i].origin = GetCities(airwaysJson.data[i].origin);
            humanReadableAirways.data[i].destination = GetCities(airwaysJson.data[i].destination);
            humanReadableAirways.data[i].price = airwaysJson.data[i].price;
            humanReadableAirways.data[i].departure_at = airwaysJson.data[i].departure_at;
            humanReadableAirways.data[i].return_at = airwaysJson.data[i].return_at;
            humanReadableAirways.data[i].duration = airwaysJson.data[i].duration;
            humanReadableAirways.data[i].duration_back = airwaysJson.data[i].duration_back;
            humanReadableAirways.data[i].duration_to = airwaysJson.data[i].duration_to;
            humanReadableAirways.data[i].transfers = airwaysJson.data[i].transfers;
            humanReadableAirways.data[i].return_transfers = airwaysJson.data[i].return_transfers;
            humanReadableAirways.data[i].flight_number = airwaysJson.data[i].flight_number;
        }

        return humanReadableAirways;
    }
*/