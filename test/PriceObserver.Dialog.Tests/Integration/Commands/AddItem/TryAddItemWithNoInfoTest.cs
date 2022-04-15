using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Tests.Integration.Commands.AddItem;

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