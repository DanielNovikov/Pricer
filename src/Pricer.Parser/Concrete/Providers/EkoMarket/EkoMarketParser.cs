using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.EkoMarket;

public class EkoMarketParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.EkoMarket;
}