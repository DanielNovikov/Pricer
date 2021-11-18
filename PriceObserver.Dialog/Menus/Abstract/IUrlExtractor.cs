using PriceObserver.Model.Telegram.Menu;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IUrlExtractor
    {
        UrlExtractionResult Extract(string str);
    }
}