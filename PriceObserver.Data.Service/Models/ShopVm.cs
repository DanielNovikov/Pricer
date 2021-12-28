using System.Collections.Generic;

namespace PriceObserver.Data.Service.Models;

public record ShopVm(string Address, string LogoFileName, IList<ItemVm> Items);