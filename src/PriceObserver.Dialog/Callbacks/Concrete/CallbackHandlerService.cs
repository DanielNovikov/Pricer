using PriceObserver.Dialog.Callbacks.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Callbacks.Concrete;

public class CallbackHandlerService : ICallbackHandlerService
{
	private readonly IAuthorizationService _authorizationService;
	private readonly IEnumerable<ICallbackHandler> _callbackHandlers;

	public CallbackHandlerService(
		IAuthorizationService authorizationService,
		IEnumerable<ICallbackHandler> callbackHandlers)
	{
		_authorizationService = authorizationService;
		_callbackHandlers = callbackHandlers;
	}

	public async Task<CallbackHandlingResult> Handle(CallbackHandlingModel callbackHandlingModel)
	{
		var user = await _authorizationService.LogIn(callbackHandlingModel.User.ExternalId);
		if (user is null)
		{
			throw new InvalidOperationException(
				@$"Could not authorize user on callback
External Id: {callbackHandlingModel.User.ExternalId}
Data: {callbackHandlingModel.Data}");
		}

		var data = JsonSerializer.Deserialize<CallbackData>(callbackHandlingModel.Data) ??
			throw new InvalidOperationException($"Could not parse callback data json: {callbackHandlingModel.Data}");
		
		var callback = new CallbackModel(data.Key, data.Parameters, user);
		var callbackHandler = _callbackHandlers.First(x => x.Key == data.Key);

		return await callbackHandler.Handle(callback);
	}
}