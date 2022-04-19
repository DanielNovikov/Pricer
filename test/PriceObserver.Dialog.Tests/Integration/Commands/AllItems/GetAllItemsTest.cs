using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data;
using PriceObserver.Data.Persistent;

namespace PriceObserver.Dialog.Tests.Integration.Commands.AllItems;

public class GetAllItemsTest : IntegrationTestingBase
{
    public static async Task Run()
    {
        var serviceModel = BuildServiceModel("Все товары ℹ");
        
        var result = await EntryPoint.Handle(serviceModel);

        var context = GetService<ApplicationDbContext>();
        var item = context.Items
            .AsNoTracking()
            .Single();

        var expectedMessage = @$"1. {item.Title}
 ├ Цена <b>{item.Price}</b> грн.
 └ <a href='{item.Url}'>Ссылка</a> на товар";
        
        result.IsSuccess.Should().BeTrue();
        result.Result.Message.Should().Be(expectedMessage);
    }
}