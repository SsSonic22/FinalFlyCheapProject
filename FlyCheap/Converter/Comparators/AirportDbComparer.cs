using FlyCheap.Models;
using FlyCheap.Models.Db;

namespace FlyCheap.Converter.Comparators;


public class AirportDbComparer : Comparer, IEqualityComparer<Airport>
{
    public bool Equals(Airport x, Airport y)
    {
        // Сравниваем по полям, которые должны определять уникальность записей
        return x.Code == y.Code && x.name == y.name && x.NameTranslationsEn == y.NameTranslationsEn;
    }

    public int GetHashCode(Airport obj)
    {
        // Возвращаем хэш-код на основе полей, которые определяют уникальность записей
        return HashCode.Combine(obj.Code, obj.name, obj.NameTranslationsEn);
    }
}