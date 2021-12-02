using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetById(long id);

        Task<IList<User>> GetAll();

        Task Add(User user);

        Task Update(User user);

        Task UpdateMenu(long id, Menu menu);
    }
}