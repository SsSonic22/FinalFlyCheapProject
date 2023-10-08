using System.ComponentModel.DataAnnotations;

namespace FlyCheap.Models.Countries;




public class Cases
{
    [Key]
    public int Id { get; set; }

    public string da { get; set; }
    public string pr { get; set; }
    public string ro { get; set; }
    public string su { get; set; }
    public string tv { get; set; }
    public string vi { get; set; }
}

/*
// Пока оставлю
public class NameTranslations
{
    [Key]
    public int Id { get; set; }

    public string en { get; set; }
}
*/

public class Countries : NamedEntity
{
    [Key]
    public int Id { get; set; }

    public string code { get; set; }
   // public string name { get; set; }
    public string currency { get; set; }

    // Навигационное свойство для связи с таблицей NameTranslations
    //public int NameTranslationsId { get; set; }
    //public string NameTranslations { get; set; }

    // Навигационное свойство для связи с таблицей Cases
    // public int CasesId { get; set; }
    // public Cases Cases { get; set; }
}
