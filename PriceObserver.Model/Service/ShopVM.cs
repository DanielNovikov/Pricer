﻿using System.Collections.Generic;

namespace PriceObserver.Model.Service
{
    public class ShopVM
    {
        public ShopVM(
            string address,
            string logoUrl,
            IList<ItemVM> items)
        {
            Address = address;
            LogoUrl = logoUrl;
            Items = items;
        }
        
        public string Address { get; }
     
        public string LogoUrl { get; }

        public IList<ItemVM> Items { get; }
    }
}