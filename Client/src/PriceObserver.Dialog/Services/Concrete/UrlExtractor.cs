using System;
using System.Text.RegularExpressions;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

public class UrlExtractor : IUrlExtractor
{
    private const string UrlRegex =
        @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b[-a-zA-Z0-9()@:%_\+.~\/#?&=]*";
        
    public UrlExtractionResult Extract(string str)
    {
        var match = Regex.Match(str, UrlRegex);

        if (!match.Success)
            return UrlExtractionResult.Fail(ResourceKey.Dialog_MessageDoesNotContainLink);

        var url = match.Groups[0].Value;

        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            ? UrlExtractionResult.Success(uri)
            : UrlExtractionResult.Fail(ResourceKey.Dialog_LinkInIncorrectFormat);
    }
}