using Converter.Models;
using Newtonsoft.Json;

namespace Converter;

public class Aviacompany
{
    public void StringToListConverter(string filePath)
    {
        string json = File.ReadAllText(filePath);
        // Console.WriteLine(json);
        List<AviacompanyJson>? aviacompanyesInput = JsonConvert.DeserializeObject<List<AviacompanyJson>>(json);

        using AirportContext aviacompanyes = new();

        for (int i = 0; i < aviacompanyesInput.Count; i++)
        {
            var AComp = aviacompanyesInput[i];

            if (aviacompanyesInput[i] != null)
            {
                aviacompanyes.Aviacompanyes.Add(AviacompanyNoDbConverter(AComp));
            }
        }
        aviacompanyes.SaveChanges();
    }

    private AviacompanyDb AviacompanyNoDbConverter(AviacompanyJson aviacompany)
    {
        if (aviacompany.iata_code == null) aviacompany.iata_code = "";
        if (aviacompany.icao_code == null) aviacompany.icao_code = "";
        if (aviacompany.name == null) aviacompany.name = "";
        
        AviacompanyDb aviacompanyDb = new AviacompanyDb
        {
            iata_code = aviacompany.iata_code,
            icao_code = aviacompany.icao_code,
            name = aviacompany.name
        };

        return aviacompanyDb;
    }
}