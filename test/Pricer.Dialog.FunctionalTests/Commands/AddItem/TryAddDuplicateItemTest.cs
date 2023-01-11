using System.Threading.Tasks;
using FluentAssertions;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Dialog.FunctionalTests.Commands.AddItem;

public class TryAddDuplicateItemTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        const string itemUrl = "https://intertop.ua/ua/product/sneakers-timberland-4816444";
        var serviceModel = BuildServiceModel(itemUrl);

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Dialog_DuplicateItem);
    }
}