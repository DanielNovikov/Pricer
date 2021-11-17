using System.Threading.Tasks;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Service.Abstract
{
    public interface IUserTokenService
    {
        Task Expire(UserToken userToken);
        
        Task<UserToken> CreateForUser(long userId);
    }
}