using System.Threading.Tasks;

namespace PriceObserver.Telegram.Dialog.Common.Abstract
{
    public interface IShopsInfoMessageBuilder
    {
        Task<string> Build();
    }
}