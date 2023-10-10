using FlyCheap.Models.Db;

namespace FlyCheap.Converter.Comparators;

public class CountriesDbComparer : Comparer, IEqualityComparer<Countries>
{
    public bool Equals(Countries x, Countries y)
    {
        // Сравниваем по полям, которые должны определять уникальность записей
        return x.name == y.name && x.code == y.code && x.currency == y.currency;
    }

    public int GetHashCode(Countries obj)
    {
        // Возвращаем хэш-код на основе полей, которые определяют уникальность записей
        return HashCode.Combine(obj.code, obj.currency, obj.name);
    }
}