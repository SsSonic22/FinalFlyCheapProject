namespace FlyCheap.Models.AirlinesJson;


// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class NameTranslations
{
    public string en { get; set; }
}

public class AirlinesJson
{
    public string name { get; set; }
    public string code { get; set; }
    public bool is_lowcost { get; set; }
    public NameTranslations name_translations { get; set; }
}

