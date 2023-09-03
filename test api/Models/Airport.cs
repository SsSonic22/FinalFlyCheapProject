namespace Test_api.Models;

public class Airport
{
    public int Id { get; set; }
    public string IataCode { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}
