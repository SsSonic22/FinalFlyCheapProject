namespace FlyCheap.Models.CountriesJson;


// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class Cases
{
    public string da { get; set; }
    public string pr { get; set; }
    public string ro { get; set; }
    public string su { get; set; }
    public string tv { get; set; }
    public string vi { get; set; }
}

public class NameTranslations
{
    public string en { get; set; }
}

public class CountriesJson
{
    public string code { get; set; }
    public string name { get; set; }
    public string currency { get; set; }
    public NameTranslations name_translations { get; set; }
    public Cases cases { get; set; }
}

