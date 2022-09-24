using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.Varus;

public class VarusContentValidator : ZakazContentValidatorBase
{
	public override ShopKey ProviderKey => ShopKey.Varus;
}