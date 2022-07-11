using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserBackgroundSettingsService
{
	Task<ReplyResult> SetPriceGrowthNotificationsEnabled(User user, bool enabled);
}