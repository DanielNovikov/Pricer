using System;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Services.Abstract;

public interface IUserItemParser
{
    ValueTask<UserItemParseResult> Parse(User user, Uri url);
}