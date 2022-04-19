using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public class UserTokenRepository : IUserTokenRepository
{
    private readonly ApplicationDbContext _context;

    public UserTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserToken> GetNotExpiredByUserId(long userId)
    {
        return await _context.UserTokens
            .AsNoTracking()
            .SingleOrDefaultAsync(x => !x.Expired && x.UserId == userId);
    }

    public async Task<UserToken> GetByToken(Guid token)
    {
        return await _context.UserTokens
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Token == token);
    }

    public async Task Update(UserToken userToken)
    {
        _context.Update(userToken);
        await _context.SaveChangesAsync();
            
        _context.DetachEntity(userToken);
    }

    public async Task Add(UserToken userToken)
    {
        await _context.AddAsync(userToken);
        await _context.SaveChangesAsync();
            
        _context.DetachEntity(userToken);
    }
}