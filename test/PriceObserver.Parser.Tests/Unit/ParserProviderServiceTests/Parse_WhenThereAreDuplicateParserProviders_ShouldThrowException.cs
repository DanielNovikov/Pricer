using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.Tests.Unit.ParserProviderServiceTests;

public class Parse_WhenThereAreDuplicateParserProviders_ShouldThrowException : Context
{
    public Parse_WhenThereAreDuplicateParserProviders_ShouldThrowException()
    {
        var parserProviders = new List<IParserProvider>
        {
            Mock.Of<IParserProvider>(x => x.ProviderKey == ProviderKey),
            Mock.Of<IParserProvider>(x => x.ProviderKey == ProviderKey)
        };

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