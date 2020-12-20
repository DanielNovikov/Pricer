using System;
using System.Collections.Generic;
using System.Linq;
using PriceObserver.Model.Telegram;
using PriceObserver.Telegram.Abstract.Commands;

namespace PriceObserver.Telegram.Concrete.Commands
{
    public class CommandParametersParser : ICommandParametersParser
    {
        private readonly ICommandParameterParser _commandParameterParser;

        public CommandParametersParser(ICommandParameterParser commandParameterParser)
        {
            _commandParameterParser = commandParameterParser;
        }

        public List<object> Parse(string message, params MessageParameterType[] parameterTypes)
        {
            var parameters = message
                .Split(" ")
                .Skip(1)
                .Select(p => p.Trim())
                .ToList();
            
            if (parameters.Count > parameterTypes.Length)
                throw new Exception($"Too many parameters. Count of parameters should be {parameterTypes.Length}");
            
            if (parameters.Count < parameterTypes.Length)
                throw new Exception($"Too few parameters. Count of parameters should be {parameterTypes.Length}");

            return Enumerable
                .Range(0, parameters.Count)
                .Select(i => _commandParameterParser.Parse(parameters[i], parameterTypes[i]))
                .ToList();
        }
    }
}