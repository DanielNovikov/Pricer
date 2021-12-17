using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Base
{
    public abstract class ParserProviderContentValidatorBase : IParserProviderContentValidator
    {
        public abstract ShopKey ProviderType { get; }

        public ContentValidatorResult Validate(IHtmlDocument document)
        {
            if (!IsPriceExists(document))
                return ContentValidatorResult.PriceDoesNotExist();

            return ContentValidatorResult.Success();
        }

        protected abstract bool IsPriceExists(IHtmlDocument document);
    }
}