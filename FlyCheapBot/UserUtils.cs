using FlyCheapBot.Db.Models;
using FlyCheapBot.State.Models;

namespace FlyCheapBot;

public static class UserUtils
{
    public static User GetOrCreate(long tgId)
    {
        var user = Context.Users.FirstOrDefault(x => x.TgId == tgId);
        if (user != null)
        {
            return user;
        }

        user = new User()
        {
            TgId = tgId,
            Role = Role.User,
            IsRegistered = true,
            InputState = InputState.Nothing
        };
        Context.Users.Add(user);
        return user;
    }
}