using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pricer.Parser.Abstract;

public interface IHttpClient
{
    Task<HttpResponseMessage> Get(Uri url, IReadOnlyDictionary<string, string> headers);
}