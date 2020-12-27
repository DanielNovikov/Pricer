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
        
        public ParsedItem Parse(IHtmlDocument htmlDocument)
        {
            _mdFashionParserContentValidator.Validate(htmlDocument);

            return _mdFashionParser.Parse(htmlDocument);
        }
    }
}