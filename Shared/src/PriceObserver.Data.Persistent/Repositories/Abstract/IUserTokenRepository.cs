using System;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IUserTokenRepository : IRepository<UserToken>
{
    Task<UserToken> GetNotExpiredByUserId(long userId);
        
    Task<UserToken> GetByToken(Guid token);
}