using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.Service.Abstract;

public interface ICommandService
{
    string GetTitle(CommandKey key);

    Command GetByTitle(string title);
}