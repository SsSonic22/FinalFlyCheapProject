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