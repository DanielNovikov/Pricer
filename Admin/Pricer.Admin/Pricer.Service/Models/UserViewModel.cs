namespace Pricer.Service.Models;

public record UserViewModel(int Id, string FullName, string UserName, string ExternalId, bool IsActive);