using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Moq;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.Tests.Unit.ParserProviderServiceTests;

public class Parse_WhenThereIsProperParserProvider_ShouldParseDocument : Context
{
    private readonly int _price;
    private readonly string _title;
    private readonly Uri _imageUrl;
    
    public Parse_WhenThereIsProperParserProvider_ShouldParseDocument()
    {
        _price = Fixture.Create<int>();
        _title = Fixture.Create<string>();
        _imageUrl = Fixture.Create<Uri>();
        
        var parserProvider = Mock.Of<IParserProvider>(x => 
            x.ProviderKey == ProviderKey &&
            x.GetPrice(Document) == _price &&
            x.GetTitle(Document) == _title &&
            x.GetImageUrl(Document) == _imageUrl); 
        
        var parserProviders = new List<IParserProvider>
        {
            parserProvider
        };

        Sut = new ParserProviderService(parserProviders);
    }

    [Fact]
    public void Test()
    {
        var result = Sut.Parse(ProviderKey, Document);

        result.Price.Should().Be(_price);
        result.Title.Should().Be(_title);
        result.ImageUrl.Should().Be(_imageUrl);
    }
}