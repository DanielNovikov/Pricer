using System;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

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
        var token = await _repository.GetNotExpiredByUserId(userId);

        if (token is not null)
            return token;
        
        token = new UserToken
        {
            Token = Guid.NewGuid(),
            Expired = false,
            UserId = userId
        };

        await _repository.Add(token);

        return token;
    }
}