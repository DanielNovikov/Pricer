using System.Threading.Tasks;

namespace Pricer.Dialog.Services.Abstract;

public interface IWebsiteLoginUrlBuilder
{
    Task<string> Build(int userId);
}