using System.Collections.Generic;
using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Models;

public record ShopCategory(ResourceKey Name, string Sign, IList<Shop> Shops) : IReadonlyEntity;