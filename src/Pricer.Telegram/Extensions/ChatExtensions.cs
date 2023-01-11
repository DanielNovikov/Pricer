using Pricer.Dialog.Models;
using Telegram.Bot.Types;

namespace Pricer.Telegram.Extensions;

public static class ChatExtensions
{
	public static UserModel ToUser(this Chat chat)
	{
		return new UserModel(
			chat.Id,
			chat.FirstName,
			chat.LastName,
			chat.Username);
	}
}