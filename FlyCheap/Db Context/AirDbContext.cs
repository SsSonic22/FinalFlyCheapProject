using Microsoft.EntityFrameworkCore;
using FlyCheap.Models;
using FlyCheap.Models.Db;


namespace FlyCheap.Db_Context;

public class AirDbContext : DbContext
{
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Cities> Cityes { get; set; }
    public DbSet<Airlines> Airlines { get; set; }
    public DbSet<Countries> Countries { get; set; }
   // public DbSet<NameTranslations> NameTranslations { get; set; }
   // public DbSet<Cases> Cases { get; set; }
    
    public static string _connectionString = "Host=localhost;Username=postgres;Password=root;Database=postgres";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}