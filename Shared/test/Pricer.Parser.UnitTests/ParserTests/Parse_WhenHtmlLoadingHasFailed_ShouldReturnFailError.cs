using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;
using Pricer.Parser.Models;
using Xunit;

namespace Pricer.Parser.UnitTests.ParserTests;

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