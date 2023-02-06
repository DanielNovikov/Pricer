using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pricer.Background.Jobs;
using Pricer.Background.Services.Abstract;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Parser.Abstract;

namespace Pricer.Background.Services.Concrete;

public class ItemsObserverService : IItemsObserverService
{
    private readonly IParser _parser;
    private readonly IItemRepository _itemRepository;
    private readonly ILogger _logger;
    private readonly Random _random;
    private readonly IItemJobService _itemJobService;

    public ItemsObserverService(
        IParser parser,
        IItemRepository itemRepository,
        ILogger<ItemsObserverService> logger, 
        IItemJobService itemJobService)
    {
        _parser = parser;
        _itemRepository = itemRepository;
        _logger = logger;
        _itemJobService = itemJobService;
        _random = new Random();
    }

    public async Task Observe()
    {
        try
        {
            var items = await _itemRepository.GetAll();

            foreach (var item in items)
            {
                try
                {
                    var parsedItemResult = await _parser.Parse(item.Url, item.ShopKey);

                    if (!parsedItemResult.IsSuccess)
                    {
                        await _itemJobService.Remove(item, parsedItemResult.Error);
                        continue;
                    }

                    var parsedItem = parsedItemResult.Result;
                    
                    var isAvailable = parsedItem.IsAvailable;
                    await _itemJobService.UpdateIsAvailable(item, isAvailable);

                    if (isAvailable)
                        await _itemJobService.Update(item, parsedItem);
                }
                catch (Exception ex)
                {
                    const string templateLogMessage = @"Background parser threw an exception
Item: {0} (Id: {1})
Item url: {2}
Message: {3}
InnerException: {4}";
                    
                    _logger.LogError(
                        ex, templateLogMessage,
                        item.Title, item.Id, item.Url, ex.Message, ex.InnerException);
                }
                finally
                {
                    var delay = _random.Next(800, 1500);
                    await Task.Delay(delay);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Unexpected error occured in {0} with exception of type {1}",
                nameof(ItemsObserver),
                ex.GetType().FullName);
        }
    }
}