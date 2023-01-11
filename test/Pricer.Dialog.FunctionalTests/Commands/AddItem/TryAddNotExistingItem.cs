using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;

namespace Pricer.Dialog.FunctionalTests.Commands.AddItem;

public class TryAddNotExistingItem : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("https://intertop.ua/ua/product/12345");

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_PageNotFound);
    }
}