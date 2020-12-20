using System;
using PriceObserver.Model.Data;
using PriceObserver.Model.Parser;

namespace PriceObserver.Telegram.Abstract.Commands.Add
{
    public interface IItemBuilder
    {
        Item Build(ParsedItem parsedItem, Uri url, long userId);
    }
}