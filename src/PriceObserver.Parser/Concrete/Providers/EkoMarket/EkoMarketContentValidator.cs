using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Concrete.Providers.Zakaz;

namespace PriceObserver.Parser.Concrete.Providers.EkoMarket;

public class EkoMarketContentValidator : ZakazContentValidatorBase
{
	public override ShopKey ProviderKey => ShopKey.EkoMarket;
}