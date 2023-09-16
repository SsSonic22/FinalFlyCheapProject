using FlyCheap.Utility_Components;

namespace FlyCheap.Models.AirportsJson;

public class Coordinates
{
    public double lat { get; set; }
    public double lon { get; set; }
}

public class NameTranslations
{
    public string en { get; set; }
}

public class AirportJson 
{
    public string city_code { get; set; }
    public string country_code { get; set; }
    public NameTranslations name_translations { get; set; }
    public string time_zone { get; set; }
    public bool flightable { get; set; }
    public Coordinates coordinates { get; set; }
    public string name { get; set; }
    public string code { get; set; }
    public string iata_type { get; set; }
}




