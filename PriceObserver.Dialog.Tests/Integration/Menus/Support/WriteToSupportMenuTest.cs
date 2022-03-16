using System.Threading.Tasks;
using FluentAssertions;

namespace PriceObserver.Dialog.Tests.Integration.Menus.Support;

public class WriteToSupportMenuTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Сообщение с вопросом");
        
        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();
        result.Result.Message.Should().Be("Спасибо за Ваше сообщение, мы с Вами скоро свяжемся! 🏃");
    }
}