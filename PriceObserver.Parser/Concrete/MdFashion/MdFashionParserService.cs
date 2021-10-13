using AngleSharp.Html.Dom;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Abstract.MdFashion;

namespace PriceObserver.Parser.Concrete.MdFashion
{
    public class MdFashionParserService : IParserProviderService
    {
        private readonly IMdFashionParserContentValidator _mdFashionParserContentValidator;
        private readonly IMdFashionParser _mdFashionParser;

        public MdFashionParserService(
            IMdFashionParserContentValidator mdFashionParserContentValidator,
            IMdFashionParser mdFashionParser)
        {
            _mdFashionParserContentValidator = mdFashionParserContentValidator;
            _mdFashionParser = mdFashionParser;
        }

        public string Host => "md-fashion.com.ua";
        
        public ParsedItemResult Parse(IHtmlDocument htmlDocument)
        {
            var validationResult = _mdFashionParserContentValidator.Validate(htmlDocument);

            if (!validationResult.IsSuccess)
                return ParsedItemResult.Fail(validationResult.Error);

            var parsedItem = _mdFashionParser.Parse(htmlDocument);
            return ParsedItemResult.Success(parsedItem);
        }
    }
}