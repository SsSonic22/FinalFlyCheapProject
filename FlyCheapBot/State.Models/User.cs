namespace FlyCheapBot.FlyCheap.State.Models;

public class User
{
    public long TgId { get; set; }
    public Role Role { get; set; }
    public InputState InputState { get; set; }
    public string TgUsername { get; set; }
    public bool IsRegistered { get; set; }
}

public enum InputState
{
    Nothing,
    DepartureСity,
    ArrivalСity,
    DepartureDate,
    FullState
}