using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Models.Enums;

namespace Pricer.Dialog.Models;

public record UserModel(
	string ExternalId,
	string FullName,
	string? Username,
	BotKey BotKey)
{
	public User ToUser()
	{
		return new User
		{
			ExternalId = ExternalId,
			FullName = FullName,
			Username = Username,
			BotKey = BotKey
		};
	}
};