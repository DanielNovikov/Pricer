﻿using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface IResourceRepository : IReadOnlyRepository<Resource, ResourceKey>
{
    Resource GetByValue(string value);
}