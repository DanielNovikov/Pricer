using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface ICommandRepository
    {
        Task<Command> GetByTitle(string title);

        Task<Command> GetByType(CommandType type);

        Task<string> GetTitleByType(CommandType type);
    }
}