using Newtonsoft.Json;

namespace Test_api;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class NameTranslations
{
    public string en { get; set; }
    public string ru { get; set; }
    public string de { get; set; }
    public string tr { get; set; }
    public string th { get; set; }
    public string it { get; set; }
    public string fr { get; set; }
    public string es { get; set; }

    [JsonProperty("zh-CN")] public string zhCN { get; set; }
    public string pl { get; set; }
    public string pt { get; set; }

    [JsonProperty("pt-BR")] public string ptBR { get; set; }
    public string lt { get; set; }
    public string jp { get; set; }

    [JsonProperty("zh-Hant")] public string zhHant { get; set; }
    public string tl { get; set; }
    public string ko { get; set; }
    public string ms { get; set; }
    public string vi { get; set; }

    [JsonProperty("zh-TW")] public string zhTW { get; set; }
    public string id { get; set; }
    public string ar { get; set; }
    public string uk { get; set; }
}

public class Countries
{
    public string name { get; set; }
    public string currency { get; set; }
    public NameTranslations name_translations { get; set; }
    public string code { get; set; }
}