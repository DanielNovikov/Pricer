using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Service.Abstract;

namespace Pricer.Data.Service.Concrete;

public class CurrencyService : ICurrencyService
{
	private readonly ICurrencyRepository _currencyRepository;
	private readonly IResourceService _resourceService;

	public CurrencyService(
		ICurrencyRepository currencyRepository,
		IResourceService resourceService)
	{
		_currencyRepository = currencyRepository;
		_resourceService = resourceService;
	}

	public string GetTitle(CurrencyKey key)
	{
		var currency = _currencyRepository.GetByKey(key);
		
		return _resourceService.Get(currency.Title);
	}
}