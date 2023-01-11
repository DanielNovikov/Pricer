using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AutoFixture;
using FluentAssertions;
using Moq;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;
using Pricer.Parser.Models;
using Xunit;

namespace Pricer.Parser.UnitTests.ParserTests;

public class Parse_WhenHtmlDocumentContentValidationHasFailed_ShouldReturnValidationError : Context
{
    private readonly ResourceKey _contentValidationError;
    
    public Parse_WhenHtmlDocumentContentValidationHasFailed_ShouldReturnValidationError()
    {
        var document = Mock.Of<IHtmlDocument>();
        var htmlLoadResult = HtmlLoadResult.Success(document);
        
        var htmlLoader = Mock.Of<IHtmlLoader>(
            x => x.Load(Url, ShopKey) == Task.FromResult(htmlLoadResult));
        
        _contentValidationError = Fixture.Create<ResourceKey>();
        var contentValidatorResult = ContentValidatorResult.Fail(_contentValidationError);
        var contentValidatorService = Mock.Of<IContentValidatorService>(
            x => x.Validate(ShopKey, document) == contentValidatorResult);
        
        Sut = new Concrete.Parser(htmlLoader, contentValidatorService, null);
    }

    [Fact]
    public async Task Test()
    {
        var result = await Sut.Parse(Url, ShopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(_contentValidationError);
        result.Result.Should().BeNull();
    }
}