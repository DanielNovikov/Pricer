using System.Net.Http;
using Pricer.Parser.Abstract;
using Pricer.Parser.Abstract.Base;

namespace Pricer.Parser.Concrete;

public class DefaultHttpClient : HttpClientBase, IDefaultHttpClient
{
    public DefaultHttpClient(HttpClient client) : base(client)
    {
    }
}