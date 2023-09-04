
using Microsoft.EntityFrameworkCore;


namespace Test_api;

public class AirportContext : DbContext
{
    public DbSet<AirportDb> Airports { get; set; }
    public static string _connectionString = "Host=localhost;Username=postgres;Password=123;Database=postgres";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Укажите строку подключения к вашей базе данных
        //optionsBuilder.UseSqlServer("YourConnectionStringHere");
        optionsBuilder.UseNpgsql(_connectionString);
    }
}