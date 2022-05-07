namespace PriceObserver.Dialog.Services.Abstract;

public interface IShopsInfoMessageBuilder
{
    string Build(int? limit = default);
}