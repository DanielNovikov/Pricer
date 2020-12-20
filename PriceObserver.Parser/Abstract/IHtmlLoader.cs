using System;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace PriceObserver.Parser.Abstract
{
    public interface IHtmlLoader
    {
        Task<IHtmlDocument> Load(Uri url);
    }
}