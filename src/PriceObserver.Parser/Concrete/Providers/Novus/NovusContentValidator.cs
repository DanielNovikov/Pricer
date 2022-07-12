using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete.Providers.Zakaz;

namespace PriceObserver.Parser.Concrete.Providers.Novus;

public class NovusContentValidator : ZakazContentValidatorBase
{
	public override ShopKey ProviderKey => ShopKey.Novus;
}