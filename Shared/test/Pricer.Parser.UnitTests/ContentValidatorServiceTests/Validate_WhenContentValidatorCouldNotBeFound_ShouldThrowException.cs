using System;
using System.Linq;
using FluentAssertions;
using Pricer.Parser.Abstract;
using Pricer.Parser.Concrete;
using Xunit;

namespace Pricer.Parser.UnitTests.ContentValidatorServiceTests;

public class Validate_WhenContentValidatorCouldNotBeFound_ShouldThrowException : Context
{
    public Validate_WhenContentValidatorCouldNotBeFound_ShouldThrowException()
    {
        var contentValidators = Enumerable.Empty<IContentValidator>();

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