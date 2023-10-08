using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FlyCheap.Models;


public class Cities : NamedEntity
{
    [Key] // Указываем, что поле 'code' будет первичным ключом
    public int id { get; set; }
    
    [Required]
    [MaxLength(3)] // Устанавливаем максимальную длину для поля 'code'
    public string code { get; set; }

    //[Required] // Указываем, что поле 'name' обязательное
    //[MaxLength(255)] // Устанавливаем максимальную длину для поля 'name'
   // public string name { get; set; }

    public double lon { get; set; }
    public double lat { get; set; } 
    
    [MaxLength(50)] // Устанавливаем максимальную длину для поля 'time_zone'
    public string time_zone { get; set; }

    [Required] // Указываем, что поле 'name_translations' обязательное
    //[Column(TypeName = "jsonb")] // Указываем тип данных для поля 'name_translations' (в PostgreSQL это jsonb)
    public string name_translations { get; set; }

    [MaxLength(2)] // Устанавливаем максимальную длину для поля 'country_code'
    public string country_code { get; set; }
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
