using System.Collections.Generic;
using FluentAssertions;
using Moq;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.UnitTests.ContentValidatorServiceTests;

public class Validate_WhenThereIsNoItemInfoOnPage_ShouldReturnNoItemInfoOnPageError : Context
{
    public Validate_WhenThereIsNoItemInfoOnPage_ShouldReturnNoItemInfoOnPageError()
    {
        var contentValidator = Mock.Of<IContentValidator>(
            x => 
                x.ProviderKey == ProviderKey && 
                x.HasItemInfo(HtmlDocument) == false);

        var contentValidators = new List<IContentValidator>
        {
            contentValidator
        };

        Sut = new ContentValidatorService(contentValidators);
    }

    [Fact]
    public void Test()
    {
        var result = Sut.Validate(ProviderKey, HtmlDocument);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_NoItemInfoOnPage);
    }
}