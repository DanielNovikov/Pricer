using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Pricer.Parser.Extensions;

public static class HtmlDocumentExtensions
{
    public static int GetMetaAsInt(this IHtmlDocument document, string propertyName)
    {
        var metaContent = GetMeta(document, propertyName);
        var content = metaContent.Replace(" ", string.Empty);

        return (int)double.Parse(content);
    }

    public static string GetMetaOgTitle(this IHtmlDocument document)
    {
        return GetMeta(document, "og:title");
    }

    public static Uri GetLinkAsImage(this IHtmlDocument document)
    {
        const string selector = "link[as='image']";

        var imageElement = document.QuerySelector<IHtmlLinkElement>(selector) ??
            throw new ArgumentNullException($"{nameof(GetLinkAsImage)}:Element");

        var imageSourceAttribute = imageElement.Href ??
            throw new ArgumentNullException($"{nameof(GetLinkAsImage)}:Content");

        if (!Uri.TryCreate(imageSourceAttribute, UriKind.Absolute, out var imageSource))
            throw new ArgumentException($"{nameof(GetLinkAsImage)}:Value");

        return imageSource;
    }

    public static string GetMeta(this IHtmlDocument document, string propertyName)
    {
        var selector = $"meta[property='{propertyName}']";

        var element = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(GetMeta)}:{propertyName}:Element");

        return element.Content?.Replace(" ", " ") ??
            throw new ArgumentNullException($"{nameof(GetMeta)}:{propertyName}:Content");;
    }
}