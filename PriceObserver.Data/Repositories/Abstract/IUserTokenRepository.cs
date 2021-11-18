using System;
using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IUserTokenRepository
    {
        Task<UserToken> GetNotExpiredByUserId(long userId);
        
        Task<UserToken> GetByToken(Guid token);

        Task Update(UserToken userToken);
        
        Task Add(UserToken userToken);
    }
}