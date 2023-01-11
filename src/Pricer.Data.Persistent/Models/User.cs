using System.Collections.Generic;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models.Abstract;

namespace Pricer.Data.Persistent.Models;

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
    
    public bool GrowthPriceNotificationsEnabled { get; set; }
    
    public int MinimumDiscountThreshold { get; set; }
        
    public IList<UserToken> Tokens { get; set; }
}