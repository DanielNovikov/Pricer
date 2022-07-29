﻿using System;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class ItemParseResultService : IItemParseResultService
{
    private readonly IItemParseResultRepository _repository;

    public ItemParseResultService(IItemParseResultRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> GetLastErrorsCount(int itemId)
    {
        var lastSucceeded = await _repository.GetLastSucceededByItemId(itemId);

        if (lastSucceeded is null)
            return default;
        
        return await _repository.GetCountOfFailedByItemId(lastSucceeded.Id, itemId);
    }

    public async Task CreateSucceeded(Item item)
    {
        await Create(item, true);
    }

    public async Task CreateFailed(Item item)
    {
        await Create(item, false);
    }

    private async Task Create(Item item, bool isSuccess)
    {
        var entity = new ItemParseResult
        {
            ItemId = item.Id,
            IsSuccess = isSuccess,
            Created = DateTime.UtcNow
        };

        await _repository.Add(entity);
    }
}