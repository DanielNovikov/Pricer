using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent;

namespace PriceObserver.Dialog.Tests.Integration.Commands.Website;

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
        token.Expired.Should().BeFalse();

        var loginUrl = $"https://pricer.ink/login/{token.Token}";
        
#if DEBUG
        var expectedMessage = $"Ссылка на сайт - {loginUrl}";
#else
        var expectedMessage = $"Нажмите <a href='{loginUrl}'>здесь</a> для перехода на сайт ⤴";        
#endif

        result.Result.Message.Should().Be(expectedMessage);
    }
}