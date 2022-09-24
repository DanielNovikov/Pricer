using System;
using System.Linq;
using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IUserLanguage _userLanguage;

    public ResourceService(
        IResourceRepository resourceRepository,
        IUserLanguage userLanguage)
    {
        _resourceRepository = resourceRepository;
        _userLanguage = userLanguage;
    }

    public string Get(ResourceKey key, params object[] parameters)
    {
        var resource = _resourceRepository.GetByKey(key);
        var languageKey = _userLanguage.LanguageKey ??
            throw new ArgumentNullException(nameof(LanguageKey));

        var resourceValue = resource.Values.Single(x => x.LanguageKey == languageKey);

        return string.Format(resourceValue.Text, parameters);
    }
}