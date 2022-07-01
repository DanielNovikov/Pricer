using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.FunctionalTests.Menus.Support;

public class GoToSupportMenuTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Поддержка 👨🏻‍💻");
        
        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();
        result.Result.Message.Should().Be("Опишите с чем вы хотели-бы обратиться 📝");

        AssertResultKeyboard(result);
        AssertUserMenu();
    }

    private static void AssertResultKeyboard(InputHandlingServiceResult result)
    {
        var buttons = result.Result.MenuKeyboard.ButtonsGrid
            .SelectMany(x => x)
            .ToList();

        buttons.Single().Title.Should().Be("Назад ◀");
    }

    private static void AssertUserMenu()
    {
        var context = GetService<ApplicationDbContext>();
        var user = context.Users
            .AsNoTracking()
            .Single();
        
        user.MenuKey.Should().Be(MenuKey.Support);
    }
}