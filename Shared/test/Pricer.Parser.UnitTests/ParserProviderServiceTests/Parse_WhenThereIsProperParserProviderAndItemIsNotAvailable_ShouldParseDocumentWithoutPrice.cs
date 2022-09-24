using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Moq;
using Pricer.Parser.Abstract;
using Pricer.Parser.Concrete;
using Xunit;

namespace Pricer.Parser.UnitTests.ParserProviderServiceTests;

public class Parse_WhenThereIsProperParserProviderAndItemIsNotAvailable_ShouldParseDocumentWithoutPrice : Context
{	
	private readonly string _title;
	private readonly Uri _imageUrl;
	private readonly bool _isAvailable;

	private readonly IParserProvider _parserProvider;
    
	public Parse_WhenThereIsProperParserProviderAndItemIsNotAvailable_ShouldParseDocumentWithoutPrice()
	{
		_title = Fixture.Create<string>();
		_imageUrl = Fixture.Create<Uri>();
		_isAvailable = false;
        
		_parserProvider = Mock.Of<IParserProvider>(x => 
			x.ProviderKey == ProviderKey &&
			x.GetTitle(Document) == _title &&
			x.GetImageUrl(Document) == _imageUrl &&
			x.IsAvailable(Document) == _isAvailable); 
        
		var parserProviders = new List<IParserProvider>
		{
			_parserProvider
		};

		Sut = new ParserProviderService(parserProviders);
	}

	[Fact]
	public void Test()
	{
		var result = Sut.Parse(ProviderKey, Document);

		result.Price.Should().Be(default);
		result.Title.Should().Be(_title);
		result.ImageUrl.Should().Be(_imageUrl);
		result.IsAvailable.Should().Be(_isAvailable);
		
		Mock
			.Get(_parserProvider)
			.Verify(
				x => x.GetPrice(Document),
				Times.Never);
	}
}