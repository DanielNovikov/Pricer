﻿using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Abstract.MdFashion;

namespace PriceObserver.Parser.Concrete.MdFashion
{
    public class MdFashionParserService : IParserProviderService
    {
        private readonly IMdFashionParserContentValidator _contentValidator;
        private readonly IMdFashionParser _parser;

        public MdFashionParserService(
            IMdFashionParserContentValidator contentValidator,
            IMdFashionParser parser)
        {
            _contentValidator = contentValidator;
            _parser = parser;
        }

        public ShopType ProviderType => ShopType.MdFashion;

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