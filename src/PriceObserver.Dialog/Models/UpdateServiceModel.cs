using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Dialog.Models;

public record UpdateServiceModel(
    string Text,
    long UserExternalId,
    string FirstName,
    string LastName, 
    string Username);
    
public static class UpdateServiceModelExtensions
{
    public static User ToUser(this UpdateServiceModel update)
    {
        return new User
        {
            ExternalId = update.UserExternalId,
            Username = update.Username,
            FirstName = update.FirstName,
            LastName = update.LastName
        };
    }
}