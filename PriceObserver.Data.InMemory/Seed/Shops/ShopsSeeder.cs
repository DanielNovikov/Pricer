using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Shops
{
    public class ShopsSeeder
    {
        public static void Seed(IMemoryCache cache)
        {
            var shops = new List<Shop>
            {
                new Shop(
                    "Intertop",
                    ShopKey.Intertop,
                    "intertop.ua",
                    new Uri("https://intertop.ua/upload/iblock/5e9/5e9af99fddb96796799d9a1523ce61ab.png")),

                new Shop(
                    "MdFashion",
                    ShopKey.MdFashion,
                    "md-fashion.com.ua",
                    new Uri(
                        "https://media-exp1.licdn.com/dms/image/C4E1BAQHdYvrPFaAN-A/company-background_10000/0/1616762724843?e=2159024400&v=beta&t=N8-gqYABqVA8ZJ9R5T5Po3w2b2orNFNneL1x4kZ2ufM")),

                new Shop(
                    "Answear",
                    ShopKey.Answear,
                    "answear.ua",
                    new Uri(
                        "https://i1.wp.com/expert.com.ua/wp-content/uploads/2015/11/logo-2.png?fit=605%2C225&ssl=1")),

                new Shop(
                    "Brocard",
                    ShopKey.Brocard,
                    "www.brocard.ua",
                    new Uri("https://cosmo-multimall.com/wp-content/uploads/2021/03/078.jpg")),

                new Shop(
                    "FARFETCH",
                    ShopKey.Farfetch,
                    "www.farfetch.com",
                    new Uri("https://logos-download.com/wp-content/uploads/2016/12/Farfetch_logo_logotype.png")),

                new Shop(
                    "MAKEUP",
                    ShopKey.Makeup,
                    "makeup.com.ua",
                    new Uri("https://u.makeup.com.ua/DTaP9LmVOYM1111111.jpg")),

                new Shop(
                    "Adidas",
                    ShopKey.Adidas,
                    "www.adidas.ua",
                    new Uri(
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1c/Adidas-group-logo-fr.svg/1200px-Adidas-group-logo-fr.svg.png")),
            };
                
            cache.Set(nameof(Shop), shops);
        }
    }
}