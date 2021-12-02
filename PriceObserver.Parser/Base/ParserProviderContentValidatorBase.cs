using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Base
{
    public abstract class ParserProviderContentValidatorBase : IParserProviderContentValidator
    {
        public abstract ShopType ProviderType { get; }

        public ContentValidatorResult Validate(IHtmlDocument document)
        {
            if (!IsPriceExists(document))
                return ContentValidatorResult.PriceDoesNotExist();

            return ContentValidatorResult.Success();
        }

        protected abstract bool IsPriceExists(IHtmlDocument document);
    }
}