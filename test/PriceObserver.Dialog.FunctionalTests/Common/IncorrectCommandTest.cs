using System.Threading.Tasks;
using FluentAssertions;

namespace PriceObserver.Dialog.FunctionalTests.Common;

public class IncorrectCommandTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("incorrect command");

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();
        result.Result.Message.Should().Be("Неверная комманда ❌");
        
        var buttons = result.Result.MenuKeyboard.Buttons
            .SelectMany(x => x)
            .ToList();

        buttons.Count.Should().Be(6);
        buttons[0].Title.Should().Be("Помощь 🆘");
        buttons[1].Title.Should().Be("Добавить ➕");
        buttons[2].Title.Should().Be("Мои товары ℹ");
        buttons[3].Title.Should().Be("Магазины 🛒");
        buttons[4].Title.Should().Be("Сайт 🌍");
        buttons[5].Title.Should().Be("Поддержка 👨🏻‍💻");
    }
}