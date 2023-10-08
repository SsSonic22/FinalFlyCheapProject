namespace FlyCheapBot.FlyCheap;

public static class Configuration
{
    private static readonly string token = "5880963661:AAFangY0hMAFEgpr14o9fxnHWEL0DDMMbAI";
    private static readonly string connectionString = "Host=localhost;Username=postgres;Password=root;Database=Maxima";

    public static string Token => token;

    public static string ConnectionString => connectionString;
}