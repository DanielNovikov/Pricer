﻿using PriceObserver.Model.Data;

namespace PriceObserver.Model.Dialog.Input
{
    public class MessageDto
    {
        public MessageDto(
            string text,
            User user)
        {
            Text = text;
            User = user;
        }

        public string Text { get; }
        
        public User User { get; }
    }
}