using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.Auchan;

public class AuchanParser : ZakazParserBase
{
	public override ShopKey ProviderKey => ShopKey.Auchan;
}