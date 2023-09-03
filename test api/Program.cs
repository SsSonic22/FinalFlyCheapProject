using Test_api.Models;

namespace Test_api;

static class Programm
{
    static void Main()
    {
        using (var context = new AirportContext())
        {
            // Получить все аэропорты
            var airports = context.Airports.ToList();

            // Добавить новый аэропорт
            var newAirport = new Airport
            {
                IataCode = "AAA",
                Name = "Some Airport",
                City = "Some City",
                Country = "Some Country",
                Latitude = 12.345678m,
                Longitude = 98.765432m
            };
            context.Airports.Add(newAirport);
            context.SaveChanges();

            // Другие операции с данными
        }

        
    }
}