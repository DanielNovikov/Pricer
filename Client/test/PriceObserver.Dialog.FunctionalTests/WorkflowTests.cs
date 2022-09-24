using System.Threading.Tasks;
using PriceObserver.Dialog.FunctionalTests.Commands.AddItem;
using PriceObserver.Dialog.FunctionalTests.Commands.AllItems;
using PriceObserver.Dialog.FunctionalTests.Commands.Help;
using PriceObserver.Dialog.FunctionalTests.Commands.Shops;
using PriceObserver.Dialog.FunctionalTests.Commands.Website;
using PriceObserver.Dialog.FunctionalTests.Common;
using PriceObserver.Dialog.FunctionalTests.Menus.Support;
using Xunit;

namespace PriceObserver.Dialog.FunctionalTests;

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