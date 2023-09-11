using System.ComponentModel.DataAnnotations;

namespace Converter.Models;

public class AviacompanyDb
{
    [Key] 
    public int Id { get; set; }
    public string name { get; set; }
    public string iata_code { get; set; }
    public string icao_code { get; set; }
}