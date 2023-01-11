using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Repositories.Abstract;

public interface ICurrencyRepository : IReadOnlyRepository<Currency, CurrencyKey>
{
}