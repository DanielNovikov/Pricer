﻿using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Dialog.Common;

namespace PriceObserver.Dialog.Common.Abstract
{
    public interface INewUserHandler
    {
        Task<ReplyResult> Handle(User user);
    }
}