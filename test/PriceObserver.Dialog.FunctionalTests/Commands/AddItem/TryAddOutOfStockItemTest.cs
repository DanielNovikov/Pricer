using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.FunctionalTests.Commands.AddItem;

public class TryAddOutOfStockItemTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("https://intertop.ua/ua/product/sneakers-clarks-4965745?tr_pr=analog");

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_OutOfStock);
    }
}