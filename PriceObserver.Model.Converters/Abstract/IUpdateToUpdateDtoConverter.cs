using PriceObserver.Model.Dialog.Input;
using Telegram.Bot.Types;

namespace PriceObserver.Model.Converters.Abstract
{
    public interface IUpdateToUpdateDtoConverter : IConverter<Update, UpdateDto>
    {
    }
}