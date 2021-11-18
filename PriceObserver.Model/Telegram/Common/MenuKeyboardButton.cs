namespace PriceObserver.Model.Telegram.Common
{
    public class MenuKeyboardButton
    {
        public string Title { get; }

        public MenuKeyboardButton(string title)
        {
            Title = title;
        }
    }
}