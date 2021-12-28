using PriceObserver.Data.Models;

namespace PriceObserver.Dialog.Input.Models;

public record UpdateDto(string Text, long UserId, string FirstName, string LastName, string Username);
    
public static class UpdateDtoExtensions
{
    public static User ToUser(this UpdateDto update)
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