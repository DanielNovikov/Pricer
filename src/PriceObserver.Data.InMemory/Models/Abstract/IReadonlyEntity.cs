namespace PriceObserver.Data.InMemory.Models.Abstract;

public interface IReadonlyEntity<TKey> : IReadonlyEntity
{
    public TKey Key { get; init; }
}

public interface IReadonlyEntity { }