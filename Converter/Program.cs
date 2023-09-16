using Converter.Models;
using Newtonsoft.Json;

namespace Converter;

static class Program
{
    // Путь к файлу JSON
    private static string _filePath = @"D:\AirCompanies.json"; //@"D:\Array.json";
    private static string _filePath2 = @"D:\Array.json";
    private static Aviacompany _aviacompany = new Aviacompany();
    private static Airports _airports = new Airports();

    static void Main()
    {
        Console.WriteLine("Start");
        // _aviacompany.Converter(_filePath);
        _airports.Converter(_filePath2);
    }
}