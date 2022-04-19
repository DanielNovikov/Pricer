using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Service.Abstract;

public interface IUserTokenService
{
    Task Expire(UserToken userToken);
        
    Task<UserToken> CreateForUser(long userId);
}