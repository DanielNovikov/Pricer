using System;

namespace PriceObserver.Common.Services.Abstract;

public interface IPartnerUrlBuilder
{
    string Build(Uri url);
}