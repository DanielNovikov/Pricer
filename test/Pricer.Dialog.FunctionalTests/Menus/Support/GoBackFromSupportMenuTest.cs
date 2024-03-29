﻿using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.FunctionalTests.Menus.Support;

public class GoBackFromSupportMenuTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Назад ◀");
        
        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();
        result.Result.Message.Should().Be("Выберите что хотите сделать ⬇");
        
        AssertUserMenu();
        AssertResultKeyboard(result);
    }

    private static void AssertUserMenu()
    {
        var context = GetService<ApplicationDbContext>();
        var user = context.Users
            .AsNoTracking()
            .Single();
        
        user.MenuKey.Should().Be(MenuKey.Home);
    }

    private static void AssertResultKeyboard(MessageHandlingResult result)
    {
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