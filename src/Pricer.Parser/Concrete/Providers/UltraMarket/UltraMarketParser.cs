using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.UltraMarket;

public class UltraMarketParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.UltraMarket;
}