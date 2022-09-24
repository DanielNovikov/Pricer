using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Concrete.Providers.Zakaz;

namespace Pricer.Parser.Concrete.Providers.UltraMarket;

public class UltraMarketContentValidator : ZakazContentValidatorBase
{
	public override ShopKey ProviderKey => ShopKey.UltraMarket;
}