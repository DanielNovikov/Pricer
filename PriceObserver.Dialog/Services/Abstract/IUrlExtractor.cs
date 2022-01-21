using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUrlExtractor
{
    UrlExtractionResult Extract(string str);
}