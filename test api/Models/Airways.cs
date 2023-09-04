namespace Test_api;

public class Airways
{
    public List<FlightData> data { get; set; }
    public string currency { get; set; }
    public bool success { get; set; }
}

public class FlightData 
{
    public string flight_number { get; set; }
    public string link { get; set; }
    public string origin_airport { get; set; }
    public string destination_airport { get; set; }
    public DateTime departure_at { get; set; }
    public string airline { get; set; }
    public string destination { get; set; }
    public DateTime return_at { get; set; }
    public string origin { get; set; }
    public int price { get; set; }
    public int return_transfers { get; set; }
    public int duration { get; set; }
    public int duration_to { get; set; }
    public int duration_back { get; set; }
    public int transfers { get; set; }
}
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);