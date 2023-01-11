using System;
using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Models;

namespace Pricer.Parser.Abstract;

public interface IHtmlLoader
{
    Task<HtmlLoadResult> Load(Uri url, ShopKey shopKey);
}