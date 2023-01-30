using Pricer.Data.Persistent.Models;

namespace Pricer.Dialog.Models;

public record UserModel(
	string ExternalId,
	string FullName,
	string? Username)
{
	public User ToUser()
	{
		return new User
		{
			ExternalId = ExternalId,
			FullName = FullName,
			Username = Username
		};
	}
};