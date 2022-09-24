using System;
using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Repositories.Abstract;

public interface IUserTokenRepository : IRepository<UserToken>
{
    Task<UserToken> GetNotExpiredByUserId(long userId);
        
    Task<UserToken> GetByToken(Guid token);
}