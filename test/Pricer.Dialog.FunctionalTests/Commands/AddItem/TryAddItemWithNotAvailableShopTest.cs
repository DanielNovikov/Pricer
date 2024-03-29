﻿using System.Threading.Tasks;
using FluentAssertions;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Dialog.FunctionalTests.Commands.AddItem;

public class TryAddItemWithNotAvailableShopTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("https://www.amazon.com/");

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Dialog_ShopIsNotAvailable);
    }
}