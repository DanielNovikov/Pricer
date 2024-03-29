﻿using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources;

public static class ResourceBuilderExtensions
{
    public static void AddResource(this IList<Resource> resources, ResourceKey key, string ukrainianText, string russianText)
    {
        var resource = new Resource(
            key, 
            new ResourceValue[]
            {
                new(ukrainianText, LanguageKey.Ukranian),
                new(russianText, LanguageKey.Russian)
            });
        
        resources.Add(resource);
    }
    
    public static void AddResource(this IList<Resource> resources, ResourceKey key, string commonText)
    {
        resources.AddResource(key, commonText, commonText);
    }
}