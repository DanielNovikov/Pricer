﻿using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Callbacks.Abstract;

public interface ICallbackHandler
{
	public CallbackKey Key { get; }
        
	public Task<CallbackHandlingResult> Handle(CallbackModel callback);
}