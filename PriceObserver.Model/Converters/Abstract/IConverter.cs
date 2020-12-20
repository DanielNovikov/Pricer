namespace PriceObserver.Model.Converters.Abstract
{
    public interface IConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source);
    }
}