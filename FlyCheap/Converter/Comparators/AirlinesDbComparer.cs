using FlyCheap.Models;
using FlyCheap.Models.Db;

namespace FlyCheap.Converter.Comparators;

public class AirlinesDbComparer  : Comparer, IEqualityComparer<Airlines>
{
    public bool Equals(Airlines x, Airlines y)
    {
        // Сравниваем по полям, которые должны определять уникальность записей
        return x.name == y.name && x.code == y.code && x.name_translations == y.name_translations;
    }

    public int GetHashCode(Airlines obj)
    {
        // Возвращаем хэш-код на основе полей, которые определяют уникальность записей
        return HashCode.Combine(obj.code, obj.name_translations, obj.name);
    }
}
