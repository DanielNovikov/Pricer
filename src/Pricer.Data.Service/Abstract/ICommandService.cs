using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.Service.Abstract;

public interface ICommandService
{
    string GetTitle(CommandKey key);

    Command GetByTitle(string title);
}