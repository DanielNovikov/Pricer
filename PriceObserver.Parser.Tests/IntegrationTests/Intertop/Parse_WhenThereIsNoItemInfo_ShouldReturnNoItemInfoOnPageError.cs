﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Tests.IntegrationTests.Base;
using Xunit;

namespace PriceObserver.Parser.Tests.IntegrationTests.Intertop;

public class Parse_WhenThereIsNoItemInfo_ShouldReturnNoItemInfoOnPageError : IntegrationTestingBase
{
    private readonly IParser _sut;

    public Parse_WhenThereIsNoItemInfo_ShouldReturnNoItemInfoOnPageError()
    {
        _sut = GetService<IParser>();
    }

    [Fact]
    public async Task Test()
    {
        var url = new Uri("https://intertop.ua/ua/");
        var shopKey = ShopKey.Intertop;

        var result = await _sut.Parse(url, shopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_NoItemInfoOnPage);
    }
}