namespace PriceObserver.Model.Data
{
    public class MenuCommand
    {
        public int Id { get; set; }
     
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        
        public int CommandId { get; set; }
        public Command Command { get; set; }
    }
}