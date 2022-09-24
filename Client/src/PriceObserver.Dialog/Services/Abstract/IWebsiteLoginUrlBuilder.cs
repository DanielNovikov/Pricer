using System.Threading.Tasks;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IWebsiteLoginUrlBuilder
{
    Task<string> Build(int userId);
}