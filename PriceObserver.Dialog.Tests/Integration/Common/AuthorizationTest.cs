using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Tests.Integration.Common;

public class AuthorizationTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("start text");
        
        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();
        
        AssertCreatedUser(serviceModel);
        
        AssertResultMessage(serviceModel, result.Result.Message);
        AssertResultKeyboard(result.Result.MenuKeyboard);
    }

    private static void AssertCreatedUser(UpdateServiceModel serviceModel)
    {
        var context = GetService<ApplicationDbContext>();
        var user = context.Users
            .AsNoTracking()
            .Single();
        
        user.Id.Should().Be(serviceModel.UserId);
        user.FirstName.Should().Be(serviceModel.FirstName);
        user.LastName.Should().Be(serviceModel.LastName);
        user.Username.Should().Be(serviceModel.Username);
        user.MenuKey.Should().Be(MenuKey.Home);
    }

    private static void AssertResultMessage(
        UpdateServiceModel serviceModel,
        string result)
    {
        var shopsInfoMessageBuilder = GetService<IShopsInfoMessageBuilder>();
        var shopsInfoMessage = shopsInfoMessageBuilder.Build();

        var expectedUserRegisteredText = $@"Приветствую, {serviceModel.FirstName} {serviceModel.LastName}! 🎉

Здесь Вы сможете добавить желаемые товары за которыми Вы хотели бы следить. Мы оповестим Вас как только цена снизится 💰.

Нажмите <b>Помощь 🆘</b> для получения дополнительной информации.

{shopsInfoMessage}

Выберите что хотите сделать ⬇";

        result.Should().Be(expectedUserRegisteredText);
    }
    
    private static void AssertResultKeyboard(MenuKeyboard resultKeyboard)
    {
        var buttons = resultKeyboard.ButtonsGrid
            .SelectMany(x => x)
            .ToList();

        buttons.Count.Should().Be(6);
        buttons[0].Title.Should().Be("Помощь 🆘");
        buttons[1].Title.Should().Be("Добавить ➕");
        buttons[2].Title.Should().Be("Все товары ℹ");
        buttons[3].Title.Should().Be("Магазины 🛒");
        buttons[4].Title.Should().Be("Сайт 🌍");
        buttons[5].Title.Should().Be("Поддержка 👨🏻‍💻");
    }
}