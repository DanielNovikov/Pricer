using Grpc.Core;

namespace PriceObserver.Web.App.Services.Abstract;

public interface IMetadataBuilder
{
    Task<Metadata?> Build();
}