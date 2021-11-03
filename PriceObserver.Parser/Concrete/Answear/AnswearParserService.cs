using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Abstract.Answear;

namespace PriceObserver.Parser.Concrete.Answear
{
    public class AnswearParserService : IParserProviderService
    {
        private readonly IAnswearParserContentValidator _contentValidator;
        private readonly IAnswearParser _parser;

        public AnswearParserService(
            IAnswearParserContentValidator contentValidator,
            IAnswearParser parser)
        {
            _contentValidator = contentValidator;
            _parser = parser;
        }

        public ShopType ProviderType => ShopType.Answear;
        
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