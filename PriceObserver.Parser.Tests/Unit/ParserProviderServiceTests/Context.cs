using AngleSharp.Html.Dom;
using AutoFixture;
using Moq;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Tests.Unit.ParserProviderServiceTests;

public abstract class Context
{
    protected readonly ShopKey ProviderKey;
    protected readonly IHtmlDocument Document;
    protected IParserProviderService Sut;

    protected readonly Fixture Fixture;
    
    protected Context()
    {   
        Fixture = new Fixture();

        ProviderKey = Fixture.Create<ShopKey>();
        Document = Mock.Of<IHtmlDocument>();
    }
}