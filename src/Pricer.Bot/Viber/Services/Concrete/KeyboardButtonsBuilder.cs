using Pricer.Bot.Viber.Models.Message;
using Pricer.Bot.Viber.Services.Abstract;
using Pricer.Dialog.Models;

namespace Pricer.Bot.Viber.Services.Concrete;

public class KeyboardButtonsBuilder : IKeyboardButtonsBuilder
{
    public KeyboardButton[] Build(MenuKeyboard keyboard)
    {
        return keyboard.Buttons
            .SelectMany(rowButtons =>
            {
                var columns = rowButtons.Count == 1 ? 6 : 3;

                return rowButtons
                    .Select(x => new KeyboardButton
                    {
                        Columns = columns,
                        Text = x.Title,
                        ActionBody = x.Title
                    });
            })
            .ToArray();
    }
}