using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pricer.Parser.Abstract.Base;

public class HttpClientBase : IHttpClient
{
    private readonly HttpClient _client;
    
    protected HttpClientBase(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<HttpResponseMessage> Get(Uri url, IReadOnlyDictionary<string, string> headers)
    {
        foreach (var (key, value) in headers)
            _client.DefaultRequestHeaders.Add(key, value);
        
        return await _client.GetAsync(url);
    }
}