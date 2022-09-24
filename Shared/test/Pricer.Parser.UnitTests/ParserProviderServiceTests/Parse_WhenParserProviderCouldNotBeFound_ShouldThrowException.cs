using System;
using System.Linq;
using FluentAssertions;
using Pricer.Parser.Abstract;
using Pricer.Parser.Concrete;
using Xunit;

namespace Pricer.Parser.UnitTests.ParserProviderServiceTests;

public class Parse_WhenParserProviderCouldNotBeFound_ShouldThrowException : Context
{
    public Parse_WhenParserProviderCouldNotBeFound_ShouldThrowException()
    {
        var parserProviders = Enumerable.Empty<IParserProvider>();

        Sut = new ParserProviderService(parserProviders);
    }

    [Fact]
    public void Test()
    {
        Sut
            .Invoking(x => x.Parse(ProviderKey, Document))
            .Should()
            .Throw<InvalidOperationException>();
    }
}