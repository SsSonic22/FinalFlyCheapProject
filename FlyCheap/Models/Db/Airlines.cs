using System.ComponentModel.DataAnnotations;

namespace FlyCheap.Models.Db;

public class Airlines : NamedEntity
{
    [Key]
    public int Id { get; set; } // Указываем, что поле 'code' будет первичным ключом
    
    [Required]
    [MaxLength(3)] // Устанавливаем максимальную длину для поля 'code'
    public string code { get; set; }

    //[Required] // Указываем, что поле 'name' обязательное
    //[MaxLength(255)] // Устанавливаем максимальную длину для поля 'name'
    //public string name { get; set; }

    public bool is_lowcost { get; set; }

    [Required] // Указываем, что поле 'name_translations' обязательное
    public string name_translations { get; set; }
}