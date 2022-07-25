using System.Collections.Generic;
using FluentAssertions;
using Moq;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.UnitTests.ContentValidatorServiceTests;

public class Validate_WhenPageHasItemInfo_ShouldReturnSuccess : Context
{
    public Validate_WhenPageHasItemInfo_ShouldReturnSuccess()
    {
        var contentValidator = Mock.Of<IContentValidator>(
            x => 
                x.ProviderKey == ProviderKey && 
                x.HasItemInfo(HtmlDocument) == true);

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

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(default);
    }
}