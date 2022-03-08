using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using AutoFixture;
using FluentAssertions;
using Moq;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Tests.Unit.HtmlLoaderTests;

public abstract class Context
{
    protected readonly Uri Url;
    protected readonly ShopKey ShopKey;
    protected IHtmlLoader Sut;

    protected HttpClient HttpClient;
    
    protected readonly Fixture Fixture;

    private const string ReferrerHeaderKey = "Referrer";
    private const string ReferrerHeaderValue = "Pricer";

    protected Context()
    {
        Fixture = new Fixture();

        Url = Fixture.Create<Uri>();
        ShopKey = Fixture.Create<ShopKey>();
    }
    
    protected IRequestHeadersBuilder BuildRequestHeadersBuilder()
    {
        var requestHeaders = new Dictionary<string, string>
        {
            {ReferrerHeaderKey, ReferrerHeaderValue}
        }.ToImmutableDictionary();
        
        return Mock.Of<IRequestHeadersBuilder>(
            x => x.Build(Url, ShopKey) == requestHeaders);
    }

    protected void ValidateHttpHeaders()
    {
        var httpClientHeaders = HttpClient.DefaultRequestHeaders;
        var referrerHeader = httpClientHeaders.Single();
        
        referrerHeader.Key.Should().Be(ReferrerHeaderKey);
        referrerHeader.Value.Single().Should().Be(ReferrerHeaderValue);
    }
}