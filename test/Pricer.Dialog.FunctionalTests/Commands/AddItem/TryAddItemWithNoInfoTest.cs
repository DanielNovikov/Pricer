﻿using System.Threading.Tasks;
using FluentAssertions;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Dialog.FunctionalTests.Commands.AddItem;

public class TryAddItemWithNoInfoTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("https://intertop.ua//");

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_NoItemInfoOnPage);
    }
}