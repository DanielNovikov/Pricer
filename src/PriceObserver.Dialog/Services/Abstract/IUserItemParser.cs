﻿using System;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserItemParser
{
    ValueTask<UserItemParseResult> Parse(User user, Uri url);
}