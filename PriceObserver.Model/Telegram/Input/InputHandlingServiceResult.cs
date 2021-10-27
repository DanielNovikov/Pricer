﻿using PriceObserver.Model.Common;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Model.Telegram.Menu;

namespace PriceObserver.Model.Telegram.Input
{
    public class InputHandlingServiceResult : ServiceResult<InputHandlingServiceResult, string, string>
    {
        public static InputHandlingServiceResult FromServiceResult(MenuInputHandlingServiceResult serviceResult)
        {
            return new InputHandlingServiceResult
            {
                IsSuccess = serviceResult.IsSuccess,
                Error = serviceResult.Error,
                Result = serviceResult.Result
            };
        }
        
        public static InputHandlingServiceResult FromServiceResult(CommandHandlingServiceResult serviceResult)
        {
            return new InputHandlingServiceResult
            {
                IsSuccess = serviceResult.IsSuccess,
                Error = serviceResult.Error,
                Result = serviceResult.Result
            };
        }
    }
}