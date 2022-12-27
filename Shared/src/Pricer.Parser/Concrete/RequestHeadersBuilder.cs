using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete;

public class RequestHeadersBuilder : IRequestHeadersBuilder
{
    private const string UserAgentKey = "User-Agent";
    private const string UserAgentValue = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";
    
    private const string AcceptKey = "accept";
    private const string AcceptValue = "text/html";
    
    private const string AcceptEncodingKey = "accept-encoding";
    private const string AcceptEncodingValue = "*";
    
    private const string AcceptLanguageKey = "accept-language";
    private const string AcceptLanguageValue = "ru-RU";
    
    public IReadOnlyDictionary<string, string> Build(Uri url, ShopKey shopKey)
    {
        var headers = new Dictionary<string, string>();

        switch (shopKey)
        {
            case ShopKey.MdFashion:
                headers.Add(UserAgentKey, UserAgentValue);
                break;
            case ShopKey.Farfetch:
                headers.Add(AcceptKey, AcceptValue);
                headers.Add(AcceptEncodingKey, AcceptEncodingValue);
                headers.Add(AcceptLanguageKey, AcceptLanguageValue);
                break;
            case ShopKey.Stylus:
                headers.Add(UserAgentKey, UserAgentValue);
                break;
            case ShopKey.Watsons:
                headers.Add(AcceptKey, AcceptValue);
                headers.Add(AcceptEncodingKey, AcceptEncodingValue);
                break;
            case ShopKey.Allo:
                headers.Add(UserAgentKey, UserAgentValue);
                break;
            case ShopKey.IHerb:
                headers.Add(UserAgentKey, UserAgentValue);
                break;
        }

        return headers.ToImmutableDictionary();
    }
}