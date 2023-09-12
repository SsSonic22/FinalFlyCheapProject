using Converter.Models;
using Converter.Utility_Components;
using Newtonsoft.Json;

namespace Converter;

public class Aviacompany
{
    public void StringToListConverter(string filePath)
    {
        string json = File.ReadAllText(filePath);
        // Console.WriteLine(json);
        List<AviacompanyJson>? aviacompanyesInput = JsonConvert.DeserializeObject<List<AviacompanyJson>>(json);

        var selectedAviacompany = aviacompanyesInput
            .Where(x => x
                .iata_code != null && x
                .name != null || x
                .iata_code != "" && x
                .name != "")
            .DistinctBy(x => new {x.iata_code, x.name, x.icao_code})
            .ToList();


        using AirportContext aviacompanyes = new();

        for (int i = 0; i < selectedAviacompany.Count; i++)
        {
            var AirlinesWithoutRepeats = RepeatCheckingForAddToDb(selectedAviacompany[i]);
            if (AirlinesWithoutRepeats is {Ok: true})
            {
                
            }

            var aviacompanyToDbConvert = AviacompanyToDbConverter(AirlinesWithoutRepeats);
            if (aviacompanyToDbConvert is {Ok: true, Content: not null})
            {
                var aviacompanyToDb = (AviacompanyDb) aviacompanyToDbConvert.Content;
                aviacompanyes.Aviacompanyes.Add(aviacompanyToDb);
            }
        }
        // aviacompanyes.SaveChanges();
    }

    private TransferObject AviacompanyToDbConverter(AviacompanyJson aviacompany)
    {
        var aviacompanyTO = new TransferObject();

        if (aviacompany.icao_code == null) aviacompany.icao_code = "";
        if (aviacompany.iata_code == null)
        {
            aviacompanyTO.Ok = false;
            return aviacompanyTO;
        }

        if (aviacompany.name == null)
        {
            aviacompanyTO.Ok = false;
            return aviacompanyTO;
        }

        AviacompanyDb aviacompanyDb = new AviacompanyDb
        {
            iata_code = aviacompany.iata_code,
            icao_code = aviacompany.icao_code,
            name = aviacompany.name
        };

        aviacompanyTO.Content = aviacompanyDb;

        return aviacompanyTO;
    }

    private TransferObject RepeatCheckingForAddToDb(AviacompanyJson aviacompany)
    {
        if (aviacompany != null)
        {
            
        }
    }

    private AviacompanyDb AviacompanyNoDbConverter1(AviacompanyJson aviacompany)
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