using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pricer.Common.Models.Options;
using Pricer.Data.Persistent.Models.Enums;
using Pricer.Dialog.Models;
using Pricer.Viber.Models;
using Pricer.Viber.Models.Message;
using Pricer.Viber.Services.Abstract;

namespace Pricer.Viber.Services.Concrete;

public class ViberBotService : IViberBotService
{
    private readonly HttpClient _httpClient;
    private readonly WebsiteOptions _websiteOptions;
    private readonly ILogger<ViberBotService> _logger;
    private readonly IKeyboardButtonsBuilder _keyboardButtonsBuilder;
    private readonly IRichMediaBuilder _richMediaBuilder;

    public BotKey Key => BotKey.Viber;
    
    public ViberBotService(
        HttpClient httpClient,
        IOptionsSnapshot<WebsiteOptions> websiteOptions,
        ILogger<ViberBotService> logger, 
        IKeyboardButtonsBuilder keyboardButtonsBuilder, 
        IRichMediaBuilder richMediaBuilder)
    {
        _httpClient = httpClient;
        _logger = logger;
        _keyboardButtonsBuilder = keyboardButtonsBuilder;
        _richMediaBuilder = richMediaBuilder;
        _websiteOptions = websiteOptions.Value;
    }

    public async Task SetWebhook()
    {
#if DEBUG
        var body = new Dictionary<string, object>
        {
            { "url", "https://6df9-95-158-53-33.eu.ngrok.io/api/viber/webhook" }
        };
#else
        var body = new Dictionary<string, object>
        {
            { "url", $"{_websiteOptions.Url}/api/viber/webhook" }
        };
#endif

        await Send(body, "set_webhook");
    }
    
    public async Task SendText(string userId, string text)
    {   
        var requestModel = new TextMessage
        {
            Text = text,
            Receiver = userId,
            Sender = new ViberUser
            {
                Name = "Pricer"
            },
            MinApiVersion = 1
        };
        
        await Send(requestModel, "send_message");
    }

    public async Task SendTextWithMenuKeyboard(string userId, string text, MenuKeyboard keyboard)
    {
        var keyboardButtons = _keyboardButtonsBuilder.Build(keyboard);
        
        var requestModel = new KeyboardMessage
        {
            Text = text,
            Receiver = userId,
            Sender = new ViberUser
            {
                Name = "Pricer"
            },
            MinApiVersion = 1,
            Keyboard = new Keyboard(keyboardButtons)
        };
        
        await Send(requestModel, "send_message");
    }

    public async Task SendTextWithMessageKeyboard(string userId, string text, MessageKeyboard keyboard)
    {
        var richMedia = _richMediaBuilder.Build(text, keyboard);
        
        var requestModel = new RichMediaMessage
        {
            Receiver = userId,
            Sender = new ViberUser
            {
                Name = "Pricer"
            },
            MinApiVersion = 6.7,
            RichMedia = richMedia
        };
        
        await Send(requestModel, "send_message");
    }

    public Task EditMessage(string userId, int messageId, string message)
    {
        throw new NotImplementedException();
    }

    public Task EditMessageWithKeyboard(string userId, int messageId, string text, MessageKeyboard keyboard)
    {
        throw new NotImplementedException();
    }

    public Task EditMessageWithInlineKeyboard(string userId, int messageId, string message)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMessage(string userId, int messageId)
    {
        throw new NotImplementedException();
    }
    
    private async Task Send<T>(T data, string method)
    {
        try
        {
            var body = JsonConvert.SerializeObject(data);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"pa/{method}", content);
            if (!response.IsSuccessStatusCode)
                _logger.LogWarning($"Could not send message to Viber by method {method}");

            var responseBody = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            // if (ex.ErrorCode != UserDeactivatedErrorCode)
            // {
            //     if (_retryAttempt == RetrySendMessageCount)
            //     {
            //         _logger.LogError("Maximum count of attempts exhausted");
            //         return;
            //     }
            //
            //     _logger.LogError(
            //         "Send error. User id: {0}, Message: {1}, Exception message: {2}",
            //         userId, 
            //         message[..MaximumMessageLengthInError],
            //         ex.Message);
            //
            //     _retryAttempt++;
            //     await Task.Delay(RetrySendMessagePause);
            //     
            //     await Send(userId, message, action);
            //         
            //     return;
            // }

            //_logger.LogInformation("User deactivated with id {0}", userId);
            // await _userService.DeactivateUserById(userId);
        }
    }
}