using PriceObserver.Model.Dialog.Menu;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IUrlExtractor
    {
        UrlExtractionResult Extract(string str);
    }
}