﻿using AngleSharp.Html.Dom;
using AutoFixture;
using Moq;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Tests.Unit.ContentValidatorServiceTests;

public abstract class Context
{
    protected readonly ShopKey ProviderKey;
    protected readonly IHtmlDocument HtmlDocument;
    protected IContentValidatorService Sut;

    protected Context()
    {   
        var fixture = new Fixture();

        ProviderKey = fixture.Create<ShopKey>();
        HtmlDocument = Mock.Of<IHtmlDocument>();
    }
}