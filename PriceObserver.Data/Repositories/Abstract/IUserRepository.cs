using System.Threading.Tasks;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetById(long id);

        Task Add(User user);

        Task Update(User user);
    }
}