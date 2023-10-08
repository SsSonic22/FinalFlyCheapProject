using System.Text.Json.Serialization;

namespace FlyCheap.Models.AirlinesJson;


// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
// Root myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
public class NameTranslations
{
    [JsonPropertyName("en")]
    public string en { get; set; }
}

public class AirlinesJson : NamedEntity
{
    //[JsonPropertyName("name")]
   // public string name { get; set; }

    [JsonPropertyName("code")]
    public string code { get; set; }

    [JsonPropertyName("is_lowcost")]
    public bool is_lowcost { get; set; }

    [JsonPropertyName("name_translations")]
    public NameTranslations name_translations { get; set; }
}



