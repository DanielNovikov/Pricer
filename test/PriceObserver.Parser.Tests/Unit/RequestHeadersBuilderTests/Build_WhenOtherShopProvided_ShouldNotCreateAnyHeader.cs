using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.Tests.Unit.RequestHeadersBuilderTests;

public class Build_WhenOtherShopProvided_ShouldNotCreateAnyHeader
{
    private readonly Uri _url;
    private readonly ShopKey[] _shopKeys;
    private readonly IRequestHeadersBuilder _sut;

    public Build_WhenOtherShopProvided_ShouldNotCreateAnyHeader()
    {
        var fixture = new Fixture();

        _url = fixture.Create<Uri>();
        _shopKeys = Enum
            .GetValues<ShopKey>()
            .Where(x =>
                x != ShopKey.Farfetch &&
                x != ShopKey.Stylus)
            .ToArray();
        
        _sut = new RequestHeadersBuilder();
    }
    
    [Fact]
    public void Test()
    {
        foreach (var shopKey in _shopKeys)
        {
            var result = _sut.Build(_url, shopKey);

            result.Count.Should().Be(0);   
        }
    }
}