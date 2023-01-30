using System.Threading.Tasks;
using Pricer.Background.Services.Abstract;
using Pricer.Bot.Abstract;
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
	private readonly IUserRepository _userRepository;
	private readonly IUserLanguage _userLanguage;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;
	private readonly IBotService _botService;

	public ItemAvailabilityService(
		IItemService itemService,
		IResourceService resourceService,
		IUserRepository userRepository,
		IUserLanguage userLanguage,
		IPartnerUrlBuilder partnerUrlBuilder, 
		IBotService botService)
	{
		_itemService = itemService;
		_resourceService = resourceService;
		_userRepository = userRepository;
		_userLanguage = userLanguage;
		_partnerUrlBuilder = partnerUrlBuilder;
		_botService = botService;
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
		await _botService.SendText(user.BotKey, user.ExternalId, availabilityMessage);
	}
}