using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Services.Abstract;

public interface IUrlExtractor
{
    UrlExtractionResult Extract(string str);
}