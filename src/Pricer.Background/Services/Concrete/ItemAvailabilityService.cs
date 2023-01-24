using System.Threading.Tasks;
using Pricer.Background.Services.Abstract;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Telegram.Abstract;

namespace Pricer.Background.Services.Concrete;

public class ItemAvailabilityService : IItemAvailabilityService
{
	private readonly IItemService _itemService;
	private readonly IResourceService _resourceService;
	private readonly ITelegramBotService _telegramBotService;
	private readonly IUserRepository _userRepository;
	private readonly IUserLanguage _userLanguage;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;

	public ItemAvailabilityService(
		IItemService itemService,
		IResourceService resourceService,
		ITelegramBotService telegramBotService,
		IUserRepository userRepository,
		IUserLanguage userLanguage,
		IPartnerUrlBuilder partnerUrlBuilder)
	{
		_itemService = itemService;
		_resourceService = resourceService;
		_telegramBotService = telegramBotService;
		_userRepository = userRepository;
		_userLanguage = userLanguage;
		_partnerUrlBuilder = partnerUrlBuilder;
	}

	public async ValueTask Update(Item item, bool isAvailable)
	{
		if (item.IsAvailable == isAvailable)
			return;

		await _itemService.UpdateAvailability(item, isAvailable);

		var user = await _userRepository.GetById(item.UserId);
		_userLanguage.Set(user.SelectedLanguageKey);
		
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		var resourceTemplate = item.IsAvailable
			? ResourceKey.Background_ItemIsInStock
			: ResourceKey.Background_ItemIsOutOfStock;
            
		var availabilityMessage = _resourceService.Get(resourceTemplate, partnerUrl, item.Title);
		await _telegramBotService.SendText(user.ExternalId, availabilityMessage);
	}
}