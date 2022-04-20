namespace PriceObserver.Data.InMemory.Models.Abstract;

public interface IReadonlyEntity<TKey>
{
    public TKey Key { get; init; }
}