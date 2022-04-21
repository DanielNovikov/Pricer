using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AutoFixture;
using FluentAssertions;
using Moq;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;
using Xunit;

namespace PriceObserver.Parser.UnitTests.ParserTests;

public class Parse_WhenHtmlDocumentIsCorrect_ShouldParseInfoParse : Context
{
    private readonly ParsedItem _parsedItem;

    public Parse_WhenHtmlDocumentIsCorrect_ShouldParseInfoParse()
    {
        var document = Mock.Of<IHtmlDocument>();
        var htmlLoadResult = HtmlLoadResult.Success(document);

        var htmlLoader = Mock.Of<IHtmlLoader>(
            x => x.Load(Url, ShopKey) == Task.FromResult(htmlLoadResult));

        var contentValidatorResult = ContentValidatorResult.Success();
        var contentValidatorService = Mock.Of<IContentValidatorService>(
            x => x.Validate(ShopKey, document) == contentValidatorResult);

        _parsedItem = Fixture.Create<ParsedItem>();
        var parserProviderService = Mock.Of<IParserProviderService>(
            x => x.Parse(ShopKey, document) == _parsedItem);
        
        Sut = new Concrete.Parser(htmlLoader, contentValidatorService, parserProviderService);
    }

    [Fact]
    public async Task Test()
    {
        var result = await Sut.Parse(Url, ShopKey);

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(default);
        result.Result.Should().Be(_parsedItem);
    }
}