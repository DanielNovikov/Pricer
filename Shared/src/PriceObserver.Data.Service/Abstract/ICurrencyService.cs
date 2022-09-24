using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.Service.Abstract;

public interface ICurrencyService
{
	string GetTitle(CurrencyKey key);
}