using System.Collections.Generic;

namespace PriceObserver.Model.Dialog.Common
{
    public class MenuKeyboard
    {
        public List<List<MenuKeyboardButton>> ButtonsGrid { get; }

        public MenuKeyboard(List<List<MenuKeyboardButton>> buttonsGrid)
        {
            ButtonsGrid = buttonsGrid;
        }
    }
}