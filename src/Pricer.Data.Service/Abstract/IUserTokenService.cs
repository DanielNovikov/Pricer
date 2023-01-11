using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Service.Abstract;

public interface IUserTokenService
{
    Task<UserToken> CreateForUser(int userId);
}