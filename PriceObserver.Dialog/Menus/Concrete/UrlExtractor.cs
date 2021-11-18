using System;
using System.Text.RegularExpressions;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Model.Dialog.Menu;

namespace PriceObserver.Dialog.Menus.Concrete
{
    public class UrlExtractor : IUrlExtractor
    {
        private const string UrlRegex =
            @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b[-a-zA-Z0-9()@:%_\+.~\/#?&=]*";
        
        public UrlExtractionResult Extract(string str)
        {
            var match = Regex.Match(str, UrlRegex);

            if (!match.Success)
                return UrlExtractionResult.Fail("В сообщении нет ссылки на товар ❌");

            var url = match.Groups[0].Value;

            return Uri.TryCreate(url, UriKind.Absolute, out var uri)
                ? UrlExtractionResult.Success(uri)
                : UrlExtractionResult.Fail("Ссылка в неверном формате ❌");
        }
    }
}