using System.Threading.Tasks;

namespace Pricer.Dialog.FunctionalTests.Menus.Support;

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

    private static void AssertResultKeyboard(MessageHandlingResult result)
    {
        var buttons = result.Result.MenuKeyboard.Buttons
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