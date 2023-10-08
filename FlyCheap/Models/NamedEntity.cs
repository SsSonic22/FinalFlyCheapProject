using System.ComponentModel.DataAnnotations;

namespace FlyCheap.Models;

public class NamedEntity
{
    [MaxLength(255)]
    public string name { get; set; }
}