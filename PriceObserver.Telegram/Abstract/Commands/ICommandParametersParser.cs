using System.Collections.Generic;
using PriceObserver.Model.Telegram;

namespace PriceObserver.Telegram.Abstract.Commands
{
    public interface ICommandParametersParser
    {
        List<object> Parse(string message, params MessageParameterType[] parameterTypes);
    }
}