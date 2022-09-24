using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.MegaMarket;

public class MegaMarketParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.MegaMarket;
}