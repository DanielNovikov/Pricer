using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Dialog.Services.Models;

public record UpdateServiceModel(string Text, long UserId, string FirstName, string LastName, string Username);
    
public static class UpdateServiceModelExtensions
{
    public static User ToUser(this UpdateServiceModel update)
    {
        return new User
        {
            Id = update.UserId,
            Username = update.Username,
            FirstName = update.FirstName,
            LastName = update.LastName
        };
    }
}