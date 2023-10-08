using FlyCheap.Converter;
using FlyCheap.Converter.Comparators;
using FlyCheap.Enums;
using FlyCheap.Models;

namespace FlyCheap.Utility_Components;

public abstract class TransferObjects
{
    public bool Ok { get; set; } = false;
}

public class ResponseContainer : TransferObjects
{
    public string content;
}
public class HttpRequestForRequestFromDataBase : TransferObjects
{
    public string file;
    public string? language;
    public string? baseUrl;
}

public class ObjectForRequestFlight : TransferObjects
{
    public List<Airport>? departureAirPorts;
    public List<Airport>? destinationAirPorts;
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
public class RequestToDbContainer
{
    public List<NamedEntity> AirInfoDbs;
    public TableCode TableCode;
}

public class ParametersContainer 
{
    
    public TableCode Table;
    public Comparer Comparer;
    public List<NamedEntity> AirInfoDbs;
}