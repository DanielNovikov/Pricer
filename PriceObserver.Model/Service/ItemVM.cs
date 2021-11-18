using PriceObserver.Model.Data;

namespace PriceObserver.Model.Service
{
    public class ItemVM
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public int Price { get; set; }
        
        public string Url { get; set; }
        
        public string ImageUrl { get; set; }
    }

    public static class ItemVMExtensions
    {
        public static ItemVM ToVM(this Item item)
        {
            return new ItemVM
            {
                Id = item.Id,
                Price = item.Price,
                Title = item.Title,
                Url = item.Url.ToString(),
                ImageUrl = item.ImageUrl.ToString()
            };
        }
    }
}