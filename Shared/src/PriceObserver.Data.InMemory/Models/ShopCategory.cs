using PriceObserver.Data.InMemory.Models.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Models;

public record ShopCategory(ResourceKey Name, string Sign, IList<Shop> Shops) : IReadonlyEntity;