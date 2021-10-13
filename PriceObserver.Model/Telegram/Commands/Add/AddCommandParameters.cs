using System;

namespace PriceObserver.Model.Telegram.Commands.Add
{
    public struct AddCommandParameters
    {
        public AddCommandParameters(Uri url)
        {
            Url = url;
        }
        
        public Uri Url { get; }
    }
}