using PriceObserver.Data.Persistent.Models;

namespace Pricer.Dialog.Models;

public record UserModel(
	long ExternalId,
	string? FirstName,
	string? LastName,
	string? Username)
{
	public User ToUser()
	{
		return new User
		{
			ExternalId = ExternalId,
			FirstName = FirstName,
			LastName = LastName,
			Username = Username
		};
	}
};