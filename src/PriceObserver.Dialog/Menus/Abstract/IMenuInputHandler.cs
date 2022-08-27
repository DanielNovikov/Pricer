﻿using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Menus.Abstract;

public interface IMenuInputHandler
{
    MenuKey Key { get; }

    Task<MenuInputHandlingServiceResult> Handle(MessageModel message);
}