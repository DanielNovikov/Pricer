using System.Threading.Tasks;
using Pricer.Data.Persistent.Models.Enums;
using Pricer.Dialog.Models;

namespace Pricer.Background.Services.Abstract;

public interface IBotService
{
    Task SendText(BotKey key, string userId, string text);

    Task SendTextWithMessageKeyboard(BotKey key, string userId, string text, MessageKeyboard keyboard);
}