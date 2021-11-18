using System.Threading.Tasks;

namespace PriceObserver.Dialog.Common.Abstract
{
    public interface IShopsInfoMessageBuilder
    {
        Task<string> Build();
    }
}