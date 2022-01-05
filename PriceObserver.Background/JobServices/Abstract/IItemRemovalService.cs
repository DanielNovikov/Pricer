﻿using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;

namespace PriceObserver.Background.JobServices.Abstract;

public interface IItemRemovalService
{
    Task Remove(Item item, ResourceKey error);
}