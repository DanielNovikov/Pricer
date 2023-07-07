using System;
using System.Text.RegularExpressions;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

public class UrlExtractor : IUrlExtractor
{
    private const string UrlRegex =
        @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b[-a-zA-Z0-9()@:%_\+.~\/#?&=]*";
        
    public UrlExtractionResult Extract(string str)
    {
        var match = Regex.Match(str, UrlRegex);

        if (!match.Success)
            return UrlExtractionResult.Fail(ResourceKey.Dialog_MessageDoesNotContainLink);

        var url = match.Groups[0].Value.Replace("https://www.", "https://");

        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            ? UrlExtractionResult.Success(uri)
            : UrlExtractionResult.Fail(ResourceKey.Dialog_LinkInIncorrectFormat);
    }
}