using System;

namespace PriceObserver.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        
        public string Text { get; set; }
        
        public DateTime Planned { get; set; }
        
        public bool Executed { get; set; }
    }
}