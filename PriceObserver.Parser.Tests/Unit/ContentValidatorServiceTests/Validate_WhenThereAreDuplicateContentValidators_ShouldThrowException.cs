using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.Tests.Unit.ContentValidatorServiceTests;

public class Validate_WhenThereAreDuplicateContentValidators_ShouldThrowException : Context
{
    public Validate_WhenThereAreDuplicateContentValidators_ShouldThrowException()
    {
        var contentValidators = new List<IContentValidator>
        {
            Mock.Of<IContentValidator>(x => x.ProviderKey == ProviderKey),
            Mock.Of<IContentValidator>(x => x.ProviderKey == ProviderKey)
        };

        Sut = new ContentValidatorService(contentValidators);
    }

    [Fact]
    public void Test()
    {
        Sut
            .Invoking(x => x.Validate(ProviderKey, HtmlDocument))
            .Should()
            .Throw<InvalidOperationException>();
    }
}