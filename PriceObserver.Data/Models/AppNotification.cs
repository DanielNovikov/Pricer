using System;

namespace PriceObserver.Data.Models
{
    public class AppNotification
    {
        public int Id { get; set; }
        
        public string Text { get; set; }
        
        public bool Executed { get; set; }
    }
}