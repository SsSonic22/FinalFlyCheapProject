using Converter.Models;

namespace Converter.Utility_Components;


public class AirportDbEqualityComparer : IEqualityComparer<AirportDb>
{
    public bool Equals(AirportDb x, AirportDb y)
    {
        // Сравниваем по полям, которые должны определять уникальность записей
        return x.IcaoCode == y.IcaoCode && x.IataCode == y.IataCode && x.NameEng == y.NameEng;
    }

    public int GetHashCode(AirportDb obj)
    {
        // Возвращаем хэш-код на основе полей, которые определяют уникальность записей
        return HashCode.Combine(obj.IcaoCode, obj.IataCode, obj.NameEng);
    }
}