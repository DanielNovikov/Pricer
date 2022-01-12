using System;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Dialog.Menus.Models;

namespace PriceObserver.Dialog.Menus.Abstract.NewItemMenu;

public interface IUserItemParser
{
    Task<UserItemParseServiceResult> Parse(User user, Uri url);
}