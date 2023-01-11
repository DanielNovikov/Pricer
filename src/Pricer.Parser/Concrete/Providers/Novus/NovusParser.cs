using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.Novus;

public class NovusParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.Novus;
}