using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Test_api;

public class AirportDb
{
    [Key] public int Id { get; set; }
    public string IataCode { get; set; }
    public string IcaoCode { get; set; }
    public string NameRus { get; set; }
    public string NameEng { get; set; }
    public string CityRus { get; set; }
    public string CityEng { get; set; }

    public double? GmtOffset { get; set; }
    public string CountryRus { get; set; }
    public string CountryEng { get; set; }
    public string IsoCode { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
}