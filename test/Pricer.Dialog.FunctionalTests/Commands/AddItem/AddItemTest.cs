using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.Persistent;

namespace Pricer.Dialog.FunctionalTests.Commands.AddItem;

public class AddItemTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        const string itemUrl = "https://intertop.ua/ua/product/sneakers-timberland-4816444";
        var serviceModel = BuildServiceModel(itemUrl);

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();
        result.Result.Message.Should().Be("Успешно добавлено! ✅");
        result.Result.MenuKeyboard.Should().BeNull();

        var context = GetService<ApplicationDbContext>();
        var item = context.Items
            .AsNoTracking()
            .Single();
        
        item.Url.Should().Be(itemUrl);
    }
}