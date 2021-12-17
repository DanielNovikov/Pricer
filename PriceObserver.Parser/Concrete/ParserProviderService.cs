using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Concrete
{
    public class ParserProviderService : IParserProviderService
    {
        private readonly IEnumerable<IParserProviderContentValidator> _contentValidators;
        private readonly IEnumerable<IParserProvider> _parsers;

        public ParserProviderService(
            IEnumerable<IParserProviderContentValidator> contentValidators, 
            IEnumerable<IParserProvider> parsers)
        {
            _contentValidators = contentValidators;
            _parsers = parsers;
        }

        public ParsedItemResult Parse(ShopKey providerType, IHtmlDocument html)
        {
            var contentValidator = GetContentValidator(providerType);
            var contentValidationResult = contentValidator.Validate(html);

            if (!contentValidationResult.IsSuccess)
                return ParsedItemResult.Fail(contentValidationResult.Error);

            var parser = GetParser(providerType);
            var parsedItem = parser.Parse(html);
            return ParsedItemResult.Success(parsedItem);
        }

        private IParserProvider GetParser(ShopKey providerType)
        {
            return _parsers.Single(x => x.ProviderType == providerType);
        }

        private IParserProviderContentValidator GetContentValidator(ShopKey providerType)
        {
            return _contentValidators.Single(p => p.ProviderType == providerType);
        }
    }
}