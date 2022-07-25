using PriceObserver.Background.Services.Abstract;
using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Background.Services.Concrete;

public class ItemAvailabilityChanger : IItemAvailabilityChanger
{
	private readonly IItemService _itemService;
	private readonly IResourceService _resourceService;
	private readonly ITelegramBotService _telegramBotService;
	private readonly IUserRepository _userRepository;
	private readonly IUserLanguage _userLanguage;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;

	public ItemAvailabilityChanger(
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

	public async ValueTask Change(Item item, bool isAvailable)
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
		await _telegramBotService.SendMessage(user.ExternalId, availabilityMessage);
	}
}