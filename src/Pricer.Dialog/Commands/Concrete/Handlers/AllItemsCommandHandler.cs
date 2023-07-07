using System;
using System.Linq;
using System.Threading.Tasks;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete.Handlers;

public class AllItemsCommandHandler : ICommandHandler
{
    private readonly IItemRepository _itemRepository;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;
    private readonly IWebsiteLoginUrlBuilder _websiteLoginUrlBuilder;
    private readonly IPartnerUrlBuilder _partnerUrlBuilder;
    private readonly ICurrencyRepository _currencyRepository;

    private const int MaximumOfItemsInMessage = 10;
    
    public AllItemsCommandHandler(
        IItemRepository itemRepository,
        IUserActionLogger userActionLogger, 
        IResourceService resourceService, 
        IWebsiteLoginUrlBuilder websiteLoginUrlBuilder, 
        IPartnerUrlBuilder partnerUrlBuilder,
        ICurrencyRepository currencyRepository)
    {
        _itemRepository = itemRepository;
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
        _websiteLoginUrlBuilder = websiteLoginUrlBuilder;
        _partnerUrlBuilder = partnerUrlBuilder;
        _currencyRepository = currencyRepository;
    }

    public CommandKey Key => CommandKey.AllItems; 
        
    public async ValueTask<IReplyResult> Handle(User user)
    {
        _userActionLogger.LogAllItemsCalled(user);
            
        var items = await _itemRepository.GetByUserIdWithLimit(user.Id, MaximumOfItemsInMessage);

        if (!items.Any())
            return new ReplyResourceResult(ResourceKey.Dialog_EmptyCart);

        var currencies = _currencyRepository.GetAll();

        var itemsInfo = items
            .Join(
                currencies,
                x => x.CurrencyKey,
                x => x.Key,
                (i, c) => new
                {
                    Title = i.Title,
                    Url = i.Url,
                    Price = i.Price,
                    CurrencyTitle = c.Title
                });
        
        var message = itemsInfo
            .Select((x, i) =>
            {
                var partnerUrl = _partnerUrlBuilder.Build(x.Url);
                return _resourceService.Get(
                    ResourceKey.Dialog_ItemInfo,
                    i + 1, x.Title, x.Price, x.CurrencyTitle, partnerUrl);
            })
            .Aggregate((x, y) => 
                $"{x}{Environment.NewLine}{Environment.NewLine}{y}");

        if (items.Count == MaximumOfItemsInMessage)
        {
            var loginUrl = await _websiteLoginUrlBuilder.Build(user.Id);
            var maximumExceededMessage = _resourceService.Get(ResourceKey.Dialog_MaximumOfItemsExceeded, loginUrl);

            message += $"{Environment.NewLine}{Environment.NewLine}{maximumExceededMessage}";
        }

        return new ReplyTextResult(message);
    }
}