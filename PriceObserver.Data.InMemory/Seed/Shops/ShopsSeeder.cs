﻿using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Shops;

public class ShopsSeeder
{
    public static void Seed(IMemoryCache cache)
    {
        var uahCurrency = new Currency(
            CurrencyKey.UAH,
            ResourceKey.Currency_UAH_Title,
            ResourceKey.Currency_UAH_Sign);
        
        var eurCurrency = new Currency(
            CurrencyKey.EUR,
            ResourceKey.Currency_EUR_Title,
            ResourceKey.Currency_EUR_Sign);
        
        var shops = new List<Shop>
        {
            new Shop(
                "Intertop",
                ShopKey.Intertop,
                "intertop.ua",
                "intertop.png",
                uahCurrency),

            new Shop(
                "MdFashion",
                ShopKey.MdFashion,
                "md-fashion.com.ua",
                "md-fashion.jfif",
                uahCurrency),

            new Shop(
                "Answear",
                ShopKey.Answear,
                "answear.ua",
                "answear.png",
                uahCurrency),

            new Shop(
                "Brocard",
                ShopKey.Brocard,
                "www.brocard.ua",
                "brocard.jpg",
                uahCurrency),

            new Shop(
                "FARFETCH",
                ShopKey.Farfetch,
                "www.farfetch.com",
                "farfetch.png",
                eurCurrency),

            new Shop(
                "MAKEUP",
                ShopKey.Makeup,
                "makeup.com.ua",
                "makeup.png",
                uahCurrency),

            new Shop(
                "Adidas",
                ShopKey.Adidas,
                "www.adidas.ua",
                "adidas.png",
                uahCurrency),
            
            new Shop(
                "Rozetka",
                ShopKey.Rozetka,
                "rozetka.com.ua",
                "rozetka.png",
                uahCurrency,
                new [] { "bt.rozetka.com.ua" }),
        };
                
        cache.Set(CacheKey.Shops, shops);
    }
}