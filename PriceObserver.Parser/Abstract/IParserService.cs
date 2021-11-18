using System;
using System.Threading.Tasks;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserService
    {
        Task<ParsedItemResult> Parse(Uri url);
    }
}