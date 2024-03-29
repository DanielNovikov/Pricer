﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.Persistent;

namespace Pricer.Dialog.FunctionalTests.Commands.Website;

public class WebsiteCommandTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Сайт 🌍");

        var result = await EntryPoint.Handle(serviceModel);

        result.IsSuccess.Should().BeTrue();

        var context = GetService<ApplicationDbContext>();

        var user = context.Users
            .AsNoTracking()
            .Include(x => x.Tokens)
            .Single();

        var token = user.Tokens.Single();
        token.Expiration.Should().BeAfter(DateTime.UtcNow);

        var loginUrl = $"https://pricer.ink/login/{token.Token}";
        
#if DEBUG
        var expectedMessage = $"Ссылка на сайт - {loginUrl}";
#else
        var expectedMessage = $"Нажмите <a href='{loginUrl}'>здесь</a> для перехода на сайт ⤴";        
#endif

        result.Result.Message.Should().Be(expectedMessage);
    }
}