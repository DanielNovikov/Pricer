using System;

namespace Pricer.Common.Services.Abstract;

public interface IPartnerUrlBuilder
{
    string Build(Uri url);
}