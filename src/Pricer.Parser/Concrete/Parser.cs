﻿using System;
using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;
using Pricer.Parser.Models;

namespace Pricer.Parser.Concrete;

public class Parser : IParser
{
    private readonly IHtmlLoader _htmlLoader;
    private readonly IContentValidatorService _contentValidatorService;
    private readonly IParserProviderService _parserProviderService;

    public Parser(
        IHtmlLoader htmlLoader, 
        IContentValidatorService contentValidatorService,
        IParserProviderService parserProviderService)
    {
        _htmlLoader = htmlLoader;
        _contentValidatorService = contentValidatorService;
        _parserProviderService = parserProviderService;
    }

    public async Task<ParsedItemServiceResult> Parse(Uri url, ShopKey shopKey)
    {
        var htmlLoadResult = await _htmlLoader.Load(url, shopKey);

        if (!htmlLoadResult.IsSuccess)
            return ParsedItemServiceResult.Fail(htmlLoadResult.Error);

        using var htmlDocument = htmlLoadResult.Result;
        var contentValidationResult = _contentValidatorService.Validate(shopKey, htmlDocument);

        if (!contentValidationResult.IsSuccess)
            return ParsedItemServiceResult.Fail(contentValidationResult.Error);

        var parsedItem = _parserProviderService.Parse(shopKey, htmlDocument);
        return ParsedItemServiceResult.Success(parsedItem);
    }
}