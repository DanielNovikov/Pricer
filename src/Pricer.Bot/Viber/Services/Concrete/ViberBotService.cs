using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pricer.Bot.Viber.Models;
using Pricer.Bot.Viber.Models.Enums;
using Pricer.Bot.Viber.Models.Message;
using Pricer.Bot.Viber.Services.Abstract;
using Pricer.Common.Models.Options;
using Pricer.Data.Persistent.Models.Enums;
using Pricer.Dialog.Models;

namespace Pricer.Bot.Viber.Services.Concrete;

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
            { "url", "https://848f-95-158-53-56.eu.ngrok.io/api/viber/webhook" }
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
        
        await Send(requestModel, "send_message", userId);
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
        
        await Send(requestModel, "send_message", userId);
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
        
        await Send(requestModel, "send_message", userId);
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
    
    private async Task Send<T>(T data, string method, string? userId = default)
    {
        try
        {
            var body = JsonConvert.SerializeObject(data);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"pa/{method}", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<ViberResponse>(responseBody);
            
            if (responseModel!.Status == ViberResponseCode.Ok)
            {
                _logger.LogWarning("Request couldn't be handled by Viber.\nMethod: {0}\nUserId: {1}\nStatus: {2}\nStatus message: {3}\nResponse: {4}",
                    method, userId, responseModel.Status, responseModel.StatusMessage, responseBody);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occured while sending message to user with id {0} by method {1}.\nException type: {2}\nMessage: {3}\nInner message: {4}",
                userId, method, ex.GetType().FullName, ex.Message, ex.InnerException);
        }
    }
}