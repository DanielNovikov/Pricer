using System;
using System.Linq;
using FluentAssertions;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.Tests.Unit.ParserProviderServiceTests;

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