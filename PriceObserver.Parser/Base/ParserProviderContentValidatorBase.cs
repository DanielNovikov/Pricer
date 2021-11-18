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

        public ContentValidatorResult Validate(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();

            if (!IsPriceExists(elements))
                return ContentValidatorResult.PriceDoesNotExist();

            return ContentValidatorResult.Success();
        }

        protected abstract bool IsPriceExists(IList<IElement> elements);
    }
}