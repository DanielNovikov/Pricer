using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Concrete.Providers.Zakaz;

namespace PriceObserver.Parser.Concrete.Providers.StolychnyiRynok;

public class StolychnyiRynokParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.StolychnyiRynok;
}