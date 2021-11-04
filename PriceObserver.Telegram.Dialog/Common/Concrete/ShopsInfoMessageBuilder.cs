using System;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Telegram.Dialog.Common.Abstract;

namespace PriceObserver.Telegram.Dialog.Common.Concrete
{
    public class ShopsInfoMessageBuilder : IShopsInfoMessageBuilder
    {
        private readonly IShopRepository _shopRepository;

        public ShopsInfoMessageBuilder(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<string> Build()
        {
            var shops = await _shopRepository.GetAll();
            
            var shopTitles = shops
                .Select(x => $"- {x.Name} ({x.Host})")
                .Aggregate((x, y) => $"{x}{Environment.NewLine}{y}");

            return $"Доступные магазины 📋{Environment.NewLine}{shopTitles}";
        }
    }
}