namespace Test_api;

public class ObjectForRequestFlight
{
    public List<AirportDb>? departureAirPorts;
    public List<AirportDb>? destinationAirPorts;
    public string departureDate;
    public string? returnDate = null;
}

public class ObjectForHttpRequest
{
    public string origin;
    public string destination;
    public string departureDate;
    public string returnDate;
}