using System.Threading.Tasks;

namespace Pricer.Dialog.FunctionalTests.Commands.Help;

public class HelpCommandTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Помощь 🆘");

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();
        
        const string expectedMessage = @"Инструкция о том как пользоваться ботом 🤖

<b>Добавить ➕</b> - инструкция по добавлению новых товаров за ценой которых Вы хотели бы следить.

<b>Мои товары ℹ</b> - показывает список добавленных Вами товаров.

<b>Магазины 🛒</b> - список доступных магазинов с которыми бот умеет работать.

<b>Сайт 🌍</b> - предоставляет ссылку на сайт, где доступно редактирование Вашего списка товаров в более удобном формате. 

<b>Поддержка 👨🏻‍💻</b> - если у вас есть какой-то вопрос, пожелание или негодование, обращайтесь в поддержку.";

        result.Result.Message.Should().Be(expectedMessage);
    }
}