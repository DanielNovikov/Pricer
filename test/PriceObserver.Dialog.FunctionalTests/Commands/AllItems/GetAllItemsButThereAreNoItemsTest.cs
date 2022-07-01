using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.FunctionalTests.Commands.AllItems;

public class GetAllItemsButThereAreNoItemsTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Мои товары ℹ");
        
        var result = await EntryPoint.Handle(serviceModel);
        
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Dialog_EmptyCart);
    }
}