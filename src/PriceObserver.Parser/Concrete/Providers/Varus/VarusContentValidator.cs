using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Concrete.Providers.Zakaz;

namespace PriceObserver.Parser.Concrete.Providers.Varus;

public class VarusContentValidator : ZakazContentValidatorBase
{
	public override ShopKey ProviderKey => ShopKey.Varus;
}