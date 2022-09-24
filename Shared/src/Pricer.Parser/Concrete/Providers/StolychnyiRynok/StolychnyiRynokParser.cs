using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.StolychnyiRynok;

public class StolychnyiRynokParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.StolychnyiRynok;
}