using System;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenRepository _repository;

        public UserTokenService(IUserTokenRepository repository)
        {
            _repository = repository;
        }

        public async Task Expire(UserToken userToken)
        {
            userToken.Expired = true;

            await _repository.Update(userToken);
        }

        public async Task<UserToken> CreateForUser(long userId)
        {
            var token = new UserToken
            {
                Token = Guid.NewGuid(),
                Expired = false,
                UserId = userId
            };

            await _repository.Add(token);

            return token;
        }
    }
}