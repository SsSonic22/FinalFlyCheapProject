namespace Test_api;

public abstract class TransferObjects
{
    public bool Ok { get; set; }
}

public class ObjectForRequestFlight : TransferObjects
{
    public List<AirportDb>? departureAirPorts;
    public List<AirportDb>? destinationAirPorts;
    public string departureDate;
    public string? returnDate = null;
}

public class ObjectForHttpRequest : TransferObjects
{
    public string origin;
    public string destination;
    public string departureDate;
    public string returnDate;
}