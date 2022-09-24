using System;
using System.Threading.Tasks;
using FluentAssertions;
using Pricer.Data.InMemory.Models.Enums;
using Xunit;

namespace Pricer.Parser.FunctionalTests;

public class ServiceTests : TestBase
{
    [Fact]
    public async Task WhenPageCouldNotBeFound_ShouldReturnPageNotFoundError()
    {
        var url = new Uri("https://intertop.ua/ua/product/not-existing-product");
        var shopKey = ShopKey.Intertop;

        var result = await Parser.Parse(url, shopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_PageNotFound);
    }
}