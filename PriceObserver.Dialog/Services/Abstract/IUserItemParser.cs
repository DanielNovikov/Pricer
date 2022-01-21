using System;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserItemParser
{
    Task<UserItemParseServiceResult> Parse(User user, Uri url);
}