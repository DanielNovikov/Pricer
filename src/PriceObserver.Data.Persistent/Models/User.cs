using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models.Abstract;

namespace PriceObserver.Data.Persistent.Models;

public class User : IAggregateRoot
{
    public int Id { get; set; }
    
    public long ExternalId { get; set; }
        
    public string FirstName { get; set; }
        
    public string LastName { get; set; }
        
    public string Username { get; set; }
        
    public bool IsActive { get; set; }
        
    public IList<Item> Items { get; set; }
        
    public MenuKey MenuKey { get; set; }
    
    public LanguageKey SelectedLanguageKey { get; set; }
        
    public IList<UserToken> Tokens { get; set; }
}