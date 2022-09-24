using Grpc.Core;

namespace Pricer.Web.App.Services.Abstract;

public interface IMetadataBuilder
{
    Task<Metadata?> Build();
}