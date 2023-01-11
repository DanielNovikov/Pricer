using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.Persistent;

namespace Pricer.Dialog.FunctionalTests.Commands.AllItems;

public class GetAllItemsTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Мои товары ℹ");
        
        var result = await EntryPoint.Handle(serviceModel);

        var context = GetService<ApplicationDbContext>();
        var item = context.Items
            .AsNoTracking()
            .Single();

        var expectedMessage = @$"1. {item.Title}
 ├ Цена <b>{item.Price}</b> грн.
 └ <a href='https://pricer.ink/view?url={item.Url}'>Ссылка</a> на товар";
        
        result.IsSuccess.Should().BeTrue();
        result.Result.Message.Should().Be(expectedMessage);
    }
}