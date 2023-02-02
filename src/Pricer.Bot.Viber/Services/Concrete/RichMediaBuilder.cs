using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Viber.Models.Message;
using Pricer.Viber.Models.Message.Enums;
using Pricer.Viber.Services.Abstract;

namespace Pricer.Viber.Services.Concrete;

public class RichMediaBuilder : IRichMediaBuilder
{
    private readonly IResourceService _resourceService;

    public RichMediaBuilder(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public RichMedia Build(string text, MessageKeyboard keyboard)
    {
        var buttons = new List<RichMediaButton>();
        
        var titleButton = new RichMediaButton
        {
            Columns = 6,
            Rows = text.Length / 40 + 1,
            ActionType = ActionType.None,
            Text = text
        };
            
        buttons.Add(titleButton);

        foreach (var rowButtons in keyboard.Buttons)
        {
            foreach (var button in rowButtons)
            {
                var buttonColumns = 6 / rowButtons.Count;

                var richMediaButton = button switch
                {
                    CallbackResourceButton callbackResourceButton => new RichMediaButton
                    {
                        Columns = buttonColumns,
                        Text = _resourceService.Get(callbackResourceButton.Resource),
                        ActionType = ActionType.Reply,
                        ActionBody = callbackResourceButton.Data
                    },
                    CallbackTextButton callbackTextButton => new RichMediaButton
                    {
                        Columns = buttonColumns,
                        Text = callbackTextButton.Text,
                        ActionType = ActionType.Reply,
                        ActionBody = callbackTextButton.Data
                    },
                    UrlButton urlButton => new RichMediaButton
                    {
                        Columns = buttonColumns,
                        Text = _resourceService.Get(urlButton.Resource),
                        ActionType = ActionType.OpenUrl,
                        ActionBody = urlButton.Url
                    },
                    _ => throw new ArgumentOutOfRangeException($"Invalid button type: {button.GetType().FullName}")
                };
                
                buttons.Add(richMediaButton);
            }
        }

        if (titleButton.Rows + keyboard.Buttons.Count > 7)
            titleButton.Rows = 7 - keyboard.Buttons.Count;
        
        return new RichMedia
        {
            Buttons = buttons.ToArray(),
            ButtonsGroupRows = keyboard.Buttons.Count + titleButton.Rows
        };
    }
}