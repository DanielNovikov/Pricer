using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Controllers.Base
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AuthorizedControllerBase : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        protected AuthorizedControllerBase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected long GetUserId()
        {
            var userIdClaim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            return long.Parse(userIdClaim.Value);
        }
        
        protected async Task<User> GetUser()
        {
            var userId = GetUserId();
            return await _userRepository.GetById(userId);
        }
    }
}