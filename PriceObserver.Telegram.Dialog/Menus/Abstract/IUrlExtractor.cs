using PriceObserver.Model.Telegram.Menu;

namespace PriceObserver.Telegram.Dialog.Menus.Abstract
{
    public interface IUrlExtractor
    {
        UrlExtractionResult Extract(string str);
    }
}