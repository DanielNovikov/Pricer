using AngleSharp.Html.Dom;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Abstract.Intertop;

namespace PriceObserver.Parser.Concrete.Intertop
{
    public class IntertopParserService : IParserProviderService
    {
        private readonly IIntertopParserContentValidator _intertopParserContentValidator;
        private readonly IIntertopParser _intertopParser;

        public IntertopParserService(
            IIntertopParserContentValidator intertopParserContentValidator,
            IIntertopParser intertopParser)
        {
            _intertopParserContentValidator = intertopParserContentValidator;
            _intertopParser = intertopParser;
        }

        public string Host => "intertop.ua";
        
        public ParsedItemResult Parse(IHtmlDocument htmlDocument)
        {
            var validationResult = _intertopParserContentValidator.Validate(htmlDocument);

            if (!validationResult.IsSuccess)
                return ParsedItemResult.Fail(validationResult.Error);

            var parsedItem = _intertopParser.Parse(htmlDocument);
            return ParsedItemResult.Success(parsedItem);
        }
    }
}