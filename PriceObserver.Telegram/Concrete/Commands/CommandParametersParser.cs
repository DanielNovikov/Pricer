using System.Linq;
using PriceObserver.Model.Telegram;
using PriceObserver.Model.Telegram.Commands;
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

        public CommandParametersParseResult Parse(string message, params MessageParameterType[] parameterTypes)
        {
            var parameters = message
                .Split(" ")
                .Skip(1)
                .Select(p => p.Trim())
                .ToList();
            
            if (parameters.Count > parameterTypes.Length)
                return CommandParametersParseResult.Fail($"Too many parameters. Count of parameters should be {parameterTypes.Length}");
            
            if (parameters.Count < parameterTypes.Length)
                return CommandParametersParseResult.Fail($"Too few parameters. Count of parameters should be {parameterTypes.Length}");
            
            var parsedParametersResult = Enumerable
                .Range(0, parameters.Count)
                .Select(i => _commandParameterParser.Parse(parameters[i], parameterTypes[i]))
                .ToList();

            if (parsedParametersResult.Any(x => !x.IsSuccess))
            {
                var error = parsedParametersResult
                    .Select(x => x.Error)
                    .Aggregate((x, y) => $"{x}\r\n{y}");

                return CommandParametersParseResult.Fail(error);
            }

            var parsedParameters = parsedParametersResult
                .Select(x => x.Result)
                .ToList();
            
            return CommandParametersParseResult.Success(parsedParameters);
        }
    }
}