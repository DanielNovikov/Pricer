using System.Threading.Tasks;
using PriceObserver.Dialog.Tests.Integration.Commands;
using PriceObserver.Dialog.Tests.Integration.Commands.AddItem;
using PriceObserver.Dialog.Tests.Integration.Commands.AllItems;
using PriceObserver.Dialog.Tests.Integration.Commands.Help;
using PriceObserver.Dialog.Tests.Integration.Commands.Shops;
using PriceObserver.Dialog.Tests.Integration.Commands.Website;
using PriceObserver.Dialog.Tests.Integration.Common;
using PriceObserver.Dialog.Tests.Integration.Menus.Support;
using Xunit;

namespace PriceObserver.Dialog.Tests.Integration;

public class WorkflowTests
{
    [Fact]
    public async Task Test()
    {
        await AuthorizationTest.Run();
        await IncorrectCommandTest.Run();

        await TryAddItemWithNoInfoTest.Run();
        await TryAddItemWithNotAvailableShopTest.Run();
        await TryAddNotExistingItem.Run();
        await TryAddOutOfStockItemTest.Run();

        await GetAllItemsButThereAreNoItemsTest.Run();
        await AddItemTest.Run();
        await TryAddDuplicateItemTest.Run();
        await GetAllItemsTest.Run();

        await HelpCommandTest.Run();

        await ShopsCommandTest.Run();

        await WebsiteCommandTest.Run();

        await GoToSupportMenuTest.Run();
        await WriteToSupportMenuTest.Run();
        await GoBackFromSupportMenuTest.Run();
    }
}