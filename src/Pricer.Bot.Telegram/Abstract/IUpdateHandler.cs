using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Pricer.Telegram.Abstract;

public interface IUpdateHandler
{
    Task Handle(Update update);
}