using System;
using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;

namespace Pricer.Data.Service.Concrete;

public class UserTokenService : IUserTokenService
{
    private readonly IUserTokenRepository _repository;

    public UserTokenService(IUserTokenRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserToken> CreateForUser(int userId)
    {
        var token = await _repository.GetNotExpiredByUserId(userId);

        if (token is not null)
            return token;
        
        token = new UserToken
        {
            Token = Guid.NewGuid(),
            Expiration = DateTime.UtcNow.AddMinutes(5),
            UserId = userId
        };

        await _repository.Add(token);

        return token;
    }
}