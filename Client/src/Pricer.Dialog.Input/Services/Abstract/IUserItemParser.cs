using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Services.Abstract;

public interface IUserItemParser
{
    ValueTask<UserItemParseResult> Parse(User user, Uri url);
}