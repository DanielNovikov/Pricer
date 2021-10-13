using System;
using System.Threading.Tasks;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserService
    {
        Task<ParsedItemResult> Parse(Uri url);
    }
}