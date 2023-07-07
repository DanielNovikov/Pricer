namespace Pricer.Models;

public record ItemPriceChangeDto(string RelativeCreated, int OldPrice, int NewPrice, string CurrencySign);