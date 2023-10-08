using System.Data.Entity;
using FlyCheap.Db_Context;

namespace FlyCheap;

public static class Catalogs
{
    public static List<string> GetCities()
    {
        using AirDbContext dbContext = new();

        return dbContext.Cityes
            .AsNoTracking()
            .Select(x => x.name)
            .ToList()
            .Union(dbContext.Cityes
                .AsNoTracking()
                .Select(x => x.name_translations)
                .ToList())
            .ToList();
    }
}