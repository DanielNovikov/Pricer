using AngleSharp.Html.Dom;
using AutoFixture;
using Moq;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.UnitTests.ParserProviderServiceTests;

public abstract class Context
{
    protected readonly ShopKey ProviderKey;
    protected readonly IHtmlDocument Document;
    protected IParserProviderService Sut = default!;

    protected readonly Fixture Fixture;
    
    protected Context()
    {   
        Fixture = new Fixture();

        ProviderKey = Fixture.Create<ShopKey>();
        Document = Mock.Of<IHtmlDocument>();
    }
}