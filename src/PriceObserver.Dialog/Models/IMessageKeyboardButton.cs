using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Models;

public interface IMessageKeyboardButton
{
	public ResourceKey Text { get; init; }
}