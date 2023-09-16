namespace FlyCheap.Models.AlliansesJson;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class NameTranslations
{
    public string en { get; set; }
}

public class AlliansesJson
{
    public List<string> airlines { get; set; }
    public NameTranslations name_translations { get; set; }
    public string name { get; set; }
}

