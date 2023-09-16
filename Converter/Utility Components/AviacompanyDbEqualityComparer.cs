using Converter.Models;

namespace Converter.Utility_Components;

public class AviacompanyDbEqualityComparer : IEqualityComparer<AviacompanyDb>
{
    public bool Equals(AviacompanyDb x, AviacompanyDb y)
    {
        // Сравниваем по полям, которые должны определять уникальность записей
        return x.iata_code == y.iata_code && x.icao_code == y.icao_code && x.name == y.name;
    }

    public int GetHashCode(AviacompanyDb obj)
    {
        // Возвращаем хэш-код на основе полей, которые определяют уникальность записей
        return HashCode.Combine(obj.icao_code, obj.iata_code, obj.name);
    }
}