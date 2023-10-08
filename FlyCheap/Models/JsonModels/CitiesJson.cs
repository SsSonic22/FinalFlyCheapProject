using Newtonsoft.Json;

namespace FlyCheap.Models.JsonModels.Cityes;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class Coordinates
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class NameTranslations
{
    public string de { get; set; }
    public string en { get; set; }

    [JsonProperty("zh-CN")]
    public string zhCN { get; set; }
    public string tr { get; set; }
    public string ru { get; set; }
    public string it { get; set; }
    public string es { get; set; }
    public string fr { get; set; }
    public string th { get; set; }
}

public class CitiesJson : NamedEntity
{
    public string code { get; set; }
   // public string name { get; set; }
    public Coordinates coordinates { get; set; }
    public string time_zone { get; set; }
    public NameTranslations name_translations { get; set; }
    public string country_code { get; set; }
}

