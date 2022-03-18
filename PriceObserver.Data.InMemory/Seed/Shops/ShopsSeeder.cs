using System.Collections.Generic;
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
                true,
                uahCurrency),

            new Shop(
                "MdFashion",
                ShopKey.MdFashion,
                "md-fashion.com.ua",
                "md-fashion.jfif",
                true,
                uahCurrency),

            new Shop(
                "Answear",
                ShopKey.Answear,
                "answear.ua",
                "answear.png",
                true,
                uahCurrency),

            new Shop(
                "Brocard",
                ShopKey.Brocard,
                "www.brocard.ua",
                "brocard.jpg",
                true,
                uahCurrency),

            new Shop(
                "FARFETCH",
                ShopKey.Farfetch,
                "www.farfetch.com",
                "farfetch.png",
                true,
                eurCurrency),

            new Shop(
                "MAKEUP",
                ShopKey.Makeup,
                "makeup.com.ua",
                "makeup.png",
                false,
                uahCurrency),

            new Shop(
                "Adidas",
                ShopKey.Adidas,
                "www.adidas.ua",
                "adidas.png",
                true,
                uahCurrency),

            new Shop(
                "Modivo",
                ShopKey.Modivo,
                "modivo.ua",
                "modivo.png",
                false,
                uahCurrency),
            
            new Shop(
                "Rozetka",
                ShopKey.Rozetka,
                "rozetka.com.ua",
                "rozetka.png",
                false,
                uahCurrency,
                new [] { "bt.rozetka.com.ua" }),
            
            new Shop(
                "Citrus",
                ShopKey.Citrus,
                "www.ctrs.com.ua",
                "citrus.png",
                false,
                uahCurrency),
            
            new Shop(
                "Stylus",
                ShopKey.Stylus,
                "stylus.ua",
                "stylus.png",
                false,
                uahCurrency),
            
            new Shop(
                "eStore",
                ShopKey.Estore,
                "estore.ua",
                "estore.png",
                true,
                uahCurrency),
            
            new Shop(
                "MOYO",
                ShopKey.Moyo,
                "www.moyo.ua",
                "moyo.png",
                false,
                uahCurrency),
            
            new Shop(
                "Comfy",
                ShopKey.Comfy,
                "comfy.ua",
                "comfy.png",
                true,
                uahCurrency),
            
            new Shop(
                "Спортмастер",
                ShopKey.Sportmaster,
                "www.sportmaster.ua",
                "sportmaster.png",
                false,
                uahCurrency)
        };
                
        cache.Set(CacheKey.Shops, shops);
    }
}