using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.Novus;

public class NovusContentValidator : ZakazContentValidatorBase
{
	public override ShopKey ProviderKey => ShopKey.Novus;
}