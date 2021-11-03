using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Abstract.Intertop;

namespace PriceObserver.Parser.Concrete.Intertop
{
    public class IntertopParserService : IParserProviderService
    {
        private readonly IIntertopParserContentValidator _contentValidator;
        private readonly IIntertopParser _parser;

        public IntertopParserService(
            IIntertopParserContentValidator contentValidator,
            IIntertopParser parser)
        {
            _contentValidator = contentValidator;
            _parser = parser;
        }

        public ShopType ProviderType => ShopType.Intertop;

        public ParsedItemResult Parse(IHtmlDocument htmlDocument)
        {
            var validationResult = _contentValidator.Validate(htmlDocument);

            if (!validationResult.IsSuccess)
                return ParsedItemResult.Fail(validationResult.Error);

            var parsedItem = _parser.Parse(htmlDocument);
            return ParsedItemResult.Success(parsedItem);
        }
    }
}