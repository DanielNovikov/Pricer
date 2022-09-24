using System;
using AutoFixture;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.UnitTests.ParserTests;

public abstract class Context
{
    protected readonly Uri Url;
    protected readonly ShopKey ShopKey;
    protected IParser Sut = default!;

    protected readonly Fixture Fixture;
    
    protected Context()
    {
        Fixture = new Fixture();

        Url = Fixture.Create<Uri>();
        ShopKey = Fixture.Create<ShopKey>();
    }
}