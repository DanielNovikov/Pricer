namespace Pricer.Service.Models;

public record UserItemViewModel(
    int Id, Uri Url, int Price, string Title, bool IsAvailable, bool IsDeleted, string Currency);