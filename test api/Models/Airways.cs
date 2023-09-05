namespace Test_api
{
    public class Airways
    {
        public List<Data> data { get; set; }
        public string currency { get; set; }
        public bool success { get; set; }
    }

    public class Data
    {
        public string flight_number { get; set; }
        public string link { get; set; }
        public string origin_airport { get; set; }
        public string destination_airport { get; set; }
        public DateTime departure_at { get; set; }
        public string airline { get; set; }
        public string destination { get; set; }
        public DateTime return_at { get; set; }
        public string origin { get; set; }
        public decimal price { get; set; } // Обновленный тип данных
        public decimal return_transfers { get; set; } // Обновленный тип данных
        public decimal duration { get; set; } // Обновленный тип данных
        public decimal duration_to { get; set; } // Обновленный тип данных
        public decimal duration_back { get; set; } // Обновленный тип данных
        public decimal transfers { get; set; } // Обновленный тип данных
    }
}