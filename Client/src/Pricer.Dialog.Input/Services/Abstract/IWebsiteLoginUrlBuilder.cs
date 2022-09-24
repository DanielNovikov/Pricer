namespace Pricer.Dialog.Input.Services.Abstract;

public interface IWebsiteLoginUrlBuilder
{
    Task<string> Build(int userId);
}