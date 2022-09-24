using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Concrete.Providers.Zakaz;

namespace PriceObserver.Parser.Concrete.Providers.UltraMarket;

public class UltraMarketContentValidator : ZakazContentValidatorBase
{
	public override ShopKey ProviderKey => ShopKey.UltraMarket;
}