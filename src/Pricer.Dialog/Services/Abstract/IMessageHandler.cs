using System.Threading.Tasks;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Services.Abstract;

public interface IMessageHandler
{
    Task<MessageHandlingResult> Handle(MessageHandlingModel messageHandlingModel);
}