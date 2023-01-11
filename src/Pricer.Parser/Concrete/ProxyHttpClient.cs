using System.Net.Http;
using Pricer.Parser.Abstract;
using Pricer.Parser.Abstract.Base;

namespace Pricer.Parser.Concrete;

public class ProxyHttpClient: HttpClientBase, IProxyHttpClient
{
    public ProxyHttpClient(HttpClient client) : base(client)
    {
    }
}