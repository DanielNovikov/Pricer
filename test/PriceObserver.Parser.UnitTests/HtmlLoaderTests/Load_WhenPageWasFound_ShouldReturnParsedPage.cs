using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.Protected;
using PriceObserver.Parser.Concrete;
using Xunit;

namespace PriceObserver.Parser.UnitTests.HtmlLoaderTests;

public class Load_WhenPageWasFound_ShouldReturnParsedPage : Context
{
    private readonly IHtmlDocument _htmlDocument;
    
    public Load_WhenPageWasFound_ShouldReturnParsedPage()
    {
        var requestHeadersBuilder = BuildRequestHeadersBuilder();

        HttpClient = BuildHttpClient();

        _htmlDocument = Mock.Of<IHtmlDocument>();
        var htmlParser = Mock.Of<IHtmlParser>(x => 
            x.ParseDocumentAsync(It.IsAny<Stream>(), CancellationToken.None) 
            == Task.FromResult(_htmlDocument));
        
        Sut = new HtmlLoader(requestHeadersBuilder, HttpClient, htmlParser);
    }

    [Fact]
    public async Task Test()
    {
        var result = await Sut.Load(Url, ShopKey);

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(default);
        result.Result.Should().Be(_htmlDocument);
        
        ValidateHttpHeaders();
    }
    
    private HttpClient BuildHttpClient()
    {
        var stream = new MemoryStream();
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StreamContent(stream)
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