namespace PriceObserver.Model.Telegram.Input
{
    public class UpdateDto
    {
        public string Text { get; set; }
        
        public long UserId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
    }
}