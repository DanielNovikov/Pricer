using PriceObserver.Dialog.Menus.Models;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IUrlExtractor
    {
        UrlExtractionResult Extract(string str);
    }
}