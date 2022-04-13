using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;

namespace PriceObserver.Web.App.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddGrpcWebClient<TClient>(this IServiceCollection services)
        where TClient : ClientBase<TClient>
    {
        return services.AddScoped<TClient>(serviceProvider =>
        {
            var grpcWebHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
            var httpClient = new HttpClient(grpcWebHandler);

            var channelOptions = new GrpcChannelOptions
            {
                HttpClient = httpClient
            };

            var navigationManager = serviceProvider.GetService<NavigationManager>() ??
                throw new ArgumentNullException(nameof(NavigationManager));
            var baseUrl = navigationManager.BaseUri;
    
            var channel = GrpcChannel.ForAddress(baseUrl, channelOptions);

            return Activator.CreateInstance(typeof(TClient), channel) as TClient ?? 
                throw new InvalidOperationException();
        });
    }
}