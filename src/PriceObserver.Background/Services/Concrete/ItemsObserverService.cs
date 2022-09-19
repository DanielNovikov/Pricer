﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Background.Jobs;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Background.Services.Concrete;

public class ItemsObserverService : IItemsObserverService
{
    private readonly IParser _parser;
    private readonly IItemRepository _itemRepository;
    private readonly IItemRemovalService _itemRemovalService;
    private readonly IItemAvailabilityService _itemAvailabilityService;
    private readonly ILogger _logger;
    private readonly Random _random;
    private readonly IItemModificationService _itemModificationService;

    public ItemsObserverService(
        IParser parser,
        IItemRepository itemRepository,
        IItemRemovalService itemRemovalService,
        IItemAvailabilityService itemAvailabilityService,
        ILogger<ItemsObserverService> logger, 
        IItemModificationService itemModificationService)
    {
        _parser = parser;
        _itemRepository = itemRepository;
        _itemRemovalService = itemRemovalService;
        _itemAvailabilityService = itemAvailabilityService;
        _logger = logger;
        _random = new Random();
        _itemModificationService = itemModificationService;
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
                        await _itemRemovalService.Remove(item, parsedItemResult.Error);
                        continue;
                    }

                    var parsedItem = parsedItemResult.Result;
                    
                    var isAvailable = parsedItem.IsAvailable;
                    await _itemAvailabilityService.Update(item, isAvailable);

                    if (isAvailable)
                        await _itemModificationService.Modify(item, parsedItem);
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