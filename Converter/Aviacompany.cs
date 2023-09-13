using System.Text;
using Converter.Models;
using Converter.Utility_Components;
using Newtonsoft.Json;

namespace Converter;

public class Aviacompany
{
    public void Converter(string filePath)
    {
        string json = File.ReadAllText(filePath);
        var ok = RepeatCheckingAndAddToDb(JsonConvert.DeserializeObject<List<AviacompanyJson>>(json));
        if (ok) Console.WriteLine("База данных обновлена!");
    }

    /// <summary>
    /// Функция проверяющая на наличие в базе данных аналогичных данных и добавляющая записи в случае их отсутствия
    /// </summary>
    /// <param name="aviacompany"></param>
    /// <returns></returns>
    private bool RepeatCheckingAndAddToDb(List<AviacompanyJson> listAviacompanyJsons)
    {
        var cleanInputData = CleaningInputData(listAviacompanyJsons);
        AviacompanyDb aviacompanyDb;

        using AviaInfoContext aviacompanyes = new();
        for (int i = 0; i < cleanInputData.Count; i++)
        {
            var recordExists = aviacompanyes.Aviacompanyes
                .Any(x => x
                    .iata_code == cleanInputData[i].iata_code && x
                    .icao_code == cleanInputData[i].icao_code && x
                    .name == cleanInputData[i].name);

            if (!recordExists)
            {
                aviacompanyDb = new AviacompanyDb()
                {
                    icao_code = cleanInputData[i].icao_code,
                    iata_code = cleanInputData[i].iata_code,
                    name = cleanInputData[i].name,
                };
                aviacompanyes.Aviacompanyes.Add(aviacompanyDb);
            }
        }

        try
        {
            aviacompanyes.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка записи в базу данных! " + e);
            return false;
        }

        return true;
    }

    private List<AviacompanyJson> CleaningInputData(List<AviacompanyJson> aviacompanyInput)
    {
        //VueAviacompanyJson(aviacompanyInput);
        var input = aviacompanyInput
            .Where(x => !string.IsNullOrEmpty(x.iata_code))
            .DistinctBy(x => new {x.iata_code, x.name, x.icao_code})
            .ToList();
        
        input.Where(x => x.icao_code == null).ToList().ForEach(x => x.icao_code = "none");
        
        return input;
    }
}