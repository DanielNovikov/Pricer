using System;
using System.Threading.Tasks;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IHtmlLoader
    {
        Task<HtmlLoadResult> Load(Uri url);
    }
}