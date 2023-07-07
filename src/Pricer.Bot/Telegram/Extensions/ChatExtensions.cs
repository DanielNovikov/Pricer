using Pricer.Data.Persistent.Models.Enums;
using Pricer.Dialog.Models;
using Telegram.Bot.Types;

namespace Pricer.Bot.Telegram.Extensions;

public static class ChatExtensions
{
	public static UserModel ToUser(this Chat chat)
	{
		var fullName = default(string);

		if (!string.IsNullOrWhiteSpace(chat.FirstName) && !string.IsNullOrWhiteSpace(chat.LastName))
			fullName = $"{chat.FirstName} {chat.LastName}";
		else if (!string.IsNullOrWhiteSpace(chat.FirstName))
			fullName = chat.FirstName;
		else
			fullName = chat.LastName;
			
		return new UserModel(
			chat.Id.ToString(),
			fullName!,
			chat.Username,
			BotKey.Telegram);
	}
}