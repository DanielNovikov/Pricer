using System.Threading.Tasks;
using Pricer.Dialog.FunctionalTests.Commands.AddItem;
using Pricer.Dialog.FunctionalTests.Commands.AllItems;
using Pricer.Dialog.FunctionalTests.Commands.Help;
using Pricer.Dialog.FunctionalTests.Commands.Shops;
using Pricer.Dialog.FunctionalTests.Commands.Website;
using Pricer.Dialog.FunctionalTests.Common;
using Pricer.Dialog.FunctionalTests.Menus.Support;
using Xunit;

namespace Pricer.Dialog.FunctionalTests;

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