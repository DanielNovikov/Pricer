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
                "Athletics",
                ShopKey.Athletics,
                "athletics.kiev.ua",
                "athletics.kiev.ua.png",
                false,
                uahCurrency),
            
            new Shop(
                "Watsons",
                ShopKey.Watsons,
                "www.watsons.ua",
                "watsons.png",
                false,
                uahCurrency),
            
            new Shop(
                "Telemart",
                ShopKey.Telemart,
                "telemart.ua",
                "telemart.png",
                false,
                uahCurrency),
            
            new Shop(
                "JYSK",
                ShopKey.Jysk,
                "jysk.ua",
                "jysk.jpg",
                false,
                uahCurrency),
            
            new Shop(
                "МегаМаркет", 
                ShopKey.MegaMarket,
                "megamarket.zakaz.ua",
                "megamarket.webp",
                true,
                uahCurrency),
            
            new Shop(
                "ЕКО Маркет", 
                ShopKey.EkoMarket,
                "eko.zakaz.ua",
                "ecomarket.jpg",
                true,
                uahCurrency),
            
            new Shop(
                "Varus", 
                ShopKey.Varus,
                "varus.zakaz.ua",
                "varus.png",
                true,
                uahCurrency),
            
            new Shop(
                "Ашан", 
                ShopKey.Auchan,
                "auchan.zakaz.ua",
                "auchan.png",
                true,
                uahCurrency),
            
            new Shop(
                "Столичний Ринок", 
                ShopKey.StolychnyiRynok,
                "stolychnyi.zakaz.ua",
                "stolychnyi.png",
                true,
                uahCurrency),
            
            new Shop(
                "Новус", 
                ShopKey.Novus,
                "novus.zakaz.ua",
                "novus.jpg",
                true,
                uahCurrency),
            
            new Shop(
                "UltraMarket", 
                ShopKey.UltraMarket,
                "ultramarket.zakaz.ua",
                "ultramarket.png",
                true,
                uahCurrency),
        };
                
        cache.Set(CacheKey.Shops, shops);
    }
}