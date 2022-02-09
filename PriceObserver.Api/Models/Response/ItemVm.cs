﻿namespace PriceObserver.Api.Models.Response;

public record ItemVm(
    int Id,
    string Title,
    int Price,
    string Url,
    string ImageUrl,
    string PriceChanges);