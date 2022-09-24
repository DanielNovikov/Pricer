using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Services.Abstract;

public interface IMessageHandler
{
    Task<MessageHandlingResult> Handle(MessageHandlingModel messageHandlingModel);
}