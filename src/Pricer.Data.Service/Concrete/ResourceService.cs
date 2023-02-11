using System;
using System.Linq;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Service.Abstract;

namespace Pricer.Data.Service.Concrete;

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

    public string GetDefault(ResourceKey key, params object[] parameters)
    {
        const LanguageKey defaultLanguage = LanguageKey.Ukranian;
        
        var resource = _resourceRepository.GetByKey(key);
        var resourceValue = resource.Values.Single(x => x.LanguageKey == defaultLanguage);

        return string.Format(resourceValue.Text, parameters);
    }
}