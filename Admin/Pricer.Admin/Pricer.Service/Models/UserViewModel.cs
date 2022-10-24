namespace Pricer.Service.Models;

public record UserViewModel(int Id, string FullName, string UserName, long ExternalId, bool IsActive);