using System;
using PriceObserver.Model.Data;
using PriceObserver.Model.Parser;
using PriceObserver.Telegram.Abstract.Commands.Add;

namespace PriceObserver.Telegram.Concrete.Commands.Add
{
    public class ItemBuilder : IItemBuilder
    {
        public Item Build(ParsedItem parsedItem, Uri url, long userId)
        {
            return new Item
            {
                Price = parsedItem.Price,
                Url = url,
                UserId = userId
            };
        }
    }
}