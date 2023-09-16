namespace FlyCheap.Models;

public class AirportDb
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string CityCode { get; set; }
    public string CountryCode { get; set; }
    public string TimeZone { get; set; }
    public bool Flightable { get; set; }
    public string Name { get; set; }
    public string IataCode { get; set; }
    public string IataType { get; set; }
}