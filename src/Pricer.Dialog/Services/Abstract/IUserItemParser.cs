using System;
using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Services.Abstract;

public interface IUserItemParser
{
    ValueTask<IReplyResult> Parse(User user, Uri url);
}