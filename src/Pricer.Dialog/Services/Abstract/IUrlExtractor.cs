using Pricer.Dialog.Models;

namespace Pricer.Dialog.Services.Abstract;

public interface IUrlExtractor
{
    UrlExtractionResult Extract(string str);
}