using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Repositories.Abstract;

namespace PriceObserver.Dialog.Tests.Integration.Commands.Shops;

public class ShopsCommandTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Магазины 🛒");

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();

        var shopsRepository = GetService<IShopRepository>();
        var shops = shopsRepository.GetAll();
        var shopsInfo = shops
            .Select(x => $"- {x.Name} ({x.Host})")
            .Aggregate((x, y) => $"{x}{Environment.NewLine}{y}");

        var expectedMessage = $"Доступные магазины 📋{Environment.NewLine}{shopsInfo}";
        
        result.Result.Message.Should().Be(expectedMessage);
    }
}