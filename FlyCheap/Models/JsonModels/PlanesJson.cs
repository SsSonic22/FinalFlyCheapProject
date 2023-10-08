namespace FlyCheap.Models;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class PlanesJson : NamedEntity
{
    public string code { get; set; }
    public string name { get; set; }
}

