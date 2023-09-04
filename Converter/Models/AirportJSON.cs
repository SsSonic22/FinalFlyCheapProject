using Newtonsoft.Json;

namespace Converter.Models;

public class Airport
{
    [JsonProperty("0")] public string IataCode { get; set; }

    [JsonProperty("1")] public string IcaoCode { get; set; }

    [JsonProperty("2")] public string NameRus { get; set; }

    [JsonProperty("3")] public string NameEng { get; set; }

    [JsonProperty("4")] public string CityRus { get; set; }

    [JsonProperty("5")] public string CityEng { get; set; }

    [JsonProperty("6")] public double? GmtOffset { get; set; }

    [JsonProperty("7")] public string CountryRus { get; set; }

    [JsonProperty("8")] public string CountryEng { get; set; }

    [JsonProperty("9")] public string IsoCode { get; set; }

    [JsonProperty("10")] public string Latitude { get; set; }

    [JsonProperty("11")] public string Longitude { get; set; }
}

public class AirportJSON
{
    public List<Airport> airports { get; set; }
}

/*
[JsonProperty("0")] public string _0 { get; set; }

    [JsonProperty("iata_code")] public string IataCode { get; set; }

    [JsonProperty("1")] public string _1 { get; set; }

    [JsonProperty("name_eng")] public string NameEng { get; set; }

    [JsonProperty("2")] public string _2 { get; set; }

    [JsonProperty("city_eng")] public string CityEng { get; set; }

    [JsonProperty("3")] public string _3 { get; set; }

    [JsonProperty("country_eng")] public string CountryEng { get; set; }

    [JsonProperty("4")] public string _4 { get; set; }

    [JsonProperty("latitude")] public string Latitude { get; set; }

    [JsonProperty("5")] public string _5 { get; set; }

    [JsonProperty("longitude")] public string Longitude { get; set; }
*/