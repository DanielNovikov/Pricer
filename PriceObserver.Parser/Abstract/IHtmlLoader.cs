using System;
using System.Threading.Tasks;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Abstract;

public interface IHtmlLoader
{
    Task<HtmlLoadResult> Load(Uri url);
}