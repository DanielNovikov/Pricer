using System;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IUserTokenRepository
{
    Task<UserToken> GetNotExpiredByUserId(long userId);
        
    Task<UserToken> GetByToken(Guid token);

    Task Update(UserToken userToken);
        
    Task Add(UserToken userToken);
}