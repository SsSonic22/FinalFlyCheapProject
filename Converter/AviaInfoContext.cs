using Microsoft.EntityFrameworkCore;
using Converter.Models;
using Microsoft.Extensions.Logging;

namespace Converter;

public class AviaInfoContext : DbContext
{
    public DbSet<AirportDb> Airports { get; set; }
    public DbSet<AviacompanyDb> Aviacompanyes { get; set; }

    public static string _connectionString = "Host=localhost;Username=postgres;Password=123;Database=postgres";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Укажите строку подключения к вашей базе данных
        optionsBuilder.UseNpgsql(_connectionString);
       // optionsBuilder.//.EnableSensitiveDataLogging();
        //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
}