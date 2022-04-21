using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;
using Xunit;

namespace PriceObserver.Parser.UnitTests.ParserTests;

public class Parse_WhenHtmlLoadingHasFailed_ShouldReturnFailError : Context
{
    private readonly ResourceKey _htmlLoadError;
    
    public Parse_WhenHtmlLoadingHasFailed_ShouldReturnFailError()
    {
        _htmlLoadError = Fixture.Create<ResourceKey>();
        var htmlLoadResult = HtmlLoadResult.Fail(_htmlLoadError);
        
        var htmlLoader = Mock.Of<IHtmlLoader>(
            x => x.Load(Url, ShopKey) == Task.FromResult(htmlLoadResult));
        
        Sut = new Concrete.Parser(htmlLoader, null, null);
    }

    [Fact]
    public async Task Test()
    {
        var result = await Sut.Parse(Url, ShopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(_htmlLoadError);
        result.Result.Should().BeNull();
    }
}