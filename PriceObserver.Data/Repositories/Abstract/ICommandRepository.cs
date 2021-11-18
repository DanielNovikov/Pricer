using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface ICommandRepository
    {
        Task<Command> GetByTitle(string title);

        Task<Command> GetByType(CommandType type);
    }
}