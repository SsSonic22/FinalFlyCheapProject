namespace FlyCheap.Models;

public class HumanReadableAirways
{
    public List<FlightDataForHumans> data { get; set; }
    public string currency { get; set; }
}

public class FlightDataForHumans
{
    public string flight_number { get; set; } //номер рейса
    
   // public string link { get; set; } на данный момент это поле не требуется
    public string origin_airport { get; set; }
    public string destination_airport { get; set; }
    public DateTime departure_at { get; set; }
    public string airline { get; set; }
    public string destination { get; set; }
    public DateTime return_at { get; set; }
    public string origin { get; set; }
    public decimal price { get; set; } 
    public decimal return_transfers { get; set; } //количество остановок на обратном пути
    public decimal duration { get; set; } 
    public decimal duration_to { get; set; } 
    public decimal duration_back { get; set; } 
    public decimal transfers { get; set; } //количество остановок по пути к месту назначения
}