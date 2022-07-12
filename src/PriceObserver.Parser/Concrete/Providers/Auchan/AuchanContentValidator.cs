using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Concrete.Providers.Zakaz;

namespace PriceObserver.Parser.Concrete.Providers.Auchan;

public class AuchanContentValidator : ZakazContentValidatorBase
{
	public override ShopKey ProviderKey => ShopKey.Auchan;
}