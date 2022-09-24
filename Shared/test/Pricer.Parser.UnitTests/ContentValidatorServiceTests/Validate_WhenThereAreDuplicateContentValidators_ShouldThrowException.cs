using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Pricer.Parser.Abstract;
using Pricer.Parser.Concrete;
using Xunit;

namespace Pricer.Parser.UnitTests.ContentValidatorServiceTests;

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