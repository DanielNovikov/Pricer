using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Concrete.Providers.Zakaz;

namespace PriceObserver.Parser.Concrete.Providers.MegaMarket;

public class MegaMarketParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.MegaMarket;
}