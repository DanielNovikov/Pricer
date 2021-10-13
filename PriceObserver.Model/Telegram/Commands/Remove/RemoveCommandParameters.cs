namespace PriceObserver.Model.Telegram.Commands.Remove
{
    public class RemoveCommandParameters
    {
        public RemoveCommandParameters(int id)
        {
            Id = id;
        }
        
        public int Id { get; }
    }
}