using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Concrete.Providers.Zakaz;

namespace PriceObserver.Parser.Concrete.Providers.Novus;

public class NovusParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.Novus;
}