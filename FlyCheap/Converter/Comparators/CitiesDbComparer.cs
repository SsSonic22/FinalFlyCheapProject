using FlyCheap.Models;
using FlyCheap.Models.Db;

namespace FlyCheap.Converter.Comparators;

public class CitiesDbComparer : Comparer,  IEqualityComparer<Cities>
{
    public bool Equals(Cities x, Cities y)
    {
        // Сравниваем по полям, которые должны определять уникальность записей
        return x.name == y.name && x.code == y.code && x.name_translations == y.name_translations;
    }

    public int GetHashCode(Cities obj)
    {
        // Возвращаем хэш-код на основе полей, которые определяют уникальность записей
        return HashCode.Combine(obj.code, obj.name, obj.name_translations);
    }   
}