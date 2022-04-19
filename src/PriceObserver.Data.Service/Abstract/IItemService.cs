﻿using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Service.Abstract;

public interface IItemService
{
    Task UpdatePrice(Item item, int price);
    
    Task<Item> Create(int price, string title, Uri url, Uri imageUrl, long userId, ShopKey shopKey);
}