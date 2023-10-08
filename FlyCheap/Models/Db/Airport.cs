using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlyCheap.Models;

public class Airport : NamedEntity
{
    [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(3)]
    public string Code { get; set; } //iata код

  //  [MaxLength(255)] public new string name { get; set; } // название аэропорта на русском


    [MaxLength(3)] 
    public string CityCode { get; set; } // код города

    [MaxLength(2)] 
    public string country_code { get; set; } // код страны

    [MaxLength(255)] 
    public string TimeZone { get; set; }

    public bool Flightable { get; set; } // используется ли

    public double Lat { get; set; }

    public double Lon { get; set; }

    [MaxLength(10)] 
    public string IataType { get; set; } //тип объекта

    [MaxLength(255)] 
    public string NameTranslationsEn { get; set; } // название на английском
}


/*
public class AirportDb
{
    [Key]
    public int Id { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string CityCode { get; set; }
    public string CountryCode { get; set; }
    public string TimeZone { get; set; }
    public bool Flightable { get; set; }
    public string Name { get; set; }
    public string NameTranslations { get; set; }
    public string IataCode { get; set; }
    public string IataType { get; set; }
}
*/