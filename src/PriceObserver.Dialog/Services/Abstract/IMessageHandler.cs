using System.Threading.Tasks;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IMessageHandler
{
    Task<MessageHandlingResult> Handle(MessageHandlingModel messageHandlingModel);
}