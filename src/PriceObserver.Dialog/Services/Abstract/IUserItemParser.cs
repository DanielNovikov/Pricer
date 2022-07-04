﻿using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserItemParser
{
    Task<UserItemParseServiceResult> Parse(User user, Uri url, ShopKey shop);
}