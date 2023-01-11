using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.Service.Abstract;

public interface ICurrencyService
{
	string GetTitle(CurrencyKey key);
}