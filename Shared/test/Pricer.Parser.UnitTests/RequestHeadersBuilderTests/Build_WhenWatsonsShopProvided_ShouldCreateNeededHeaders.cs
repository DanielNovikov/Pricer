using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;
using Pricer.Parser.Concrete;
using Xunit;

namespace Pricer.Parser.UnitTests.RequestHeadersBuilderTests;

public class Build_WhenWatsonsShopProvided_ShouldCreateNeededHeaders
{
    private readonly Uri _url;
    private readonly ShopKey _shopKey;
    private readonly IRequestHeadersBuilder _sut;

    public Build_WhenWatsonsShopProvided_ShouldCreateNeededHeaders()
    {
        var fixture = new Fixture();

        _url = fixture.Create<Uri>();
        _shopKey = ShopKey.Watsons;
        
        _sut = new RequestHeadersBuilder();
    }
    
    [Fact]
    public void Test()
    {
        var result = _sut.Build(_url, _shopKey);

        result.Count.Should().Be(2);

        result.Any(x => x.Key == "accept" && x.Value == "text/html").Should().BeTrue();
        result.Any(x => x.Key == "accept-encoding" && x.Value == "*").Should().BeTrue();
    }
}