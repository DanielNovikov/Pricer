using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.Protected;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.Tests.Unit.HtmlLoaderTests;

public class Load_WhenPageCouldNotBeFound_ShouldReturnProperError : Context
{
    public Load_WhenPageCouldNotBeFound_ShouldReturnProperError()
    {
        var requestHeadersBuilder = BuildRequestHeadersBuilder();
        HttpClient = BuildHttpClient();

        Sut = new HtmlLoader(requestHeadersBuilder, HttpClient, null);
    }

    [Fact]
    public async Task Test()
    {
        var result = await Sut.Load(Url, ShopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_PageNotFound);
        result.Result.Should().BeNull();
        
        ValidateHttpHeaders();
    }
    
    private HttpClient BuildHttpClient()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        };
        var messageHandler = Mock.Of<HttpMessageHandler>();
        Mock
            .Get(messageHandler)
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        return new HttpClient(messageHandler)
        {
            BaseAddress = Fixture.Create<Uri>()
        };
    }
}