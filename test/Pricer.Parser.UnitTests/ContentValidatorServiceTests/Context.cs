using AngleSharp.Html.Dom;
using AutoFixture;
using Moq;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.UnitTests.ContentValidatorServiceTests;

public abstract class Context
{
    protected readonly ShopKey ProviderKey;
    protected readonly IHtmlDocument HtmlDocument;
    protected IContentValidatorService Sut = default!;

    protected Context()
    {   
        var fixture = new Fixture();

        ProviderKey = fixture.Create<ShopKey>();
        HtmlDocument = Mock.Of<IHtmlDocument>();
    }
}