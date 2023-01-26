using System.Threading.Tasks;
using Pricer.Dialog.Models;

namespace Pricer.Telegram.Abstract;

public interface ITelegramMessageHandler
{
    Task Handle(MessageHandlingModel messageHandlingModel);
}