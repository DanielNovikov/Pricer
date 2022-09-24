using PriceObserver.Data.Persistent.Models;
using System.Threading.Tasks;

namespace PriceObserver.Background.Services.Abstract;

public interface IItemAvailabilityService
{
	ValueTask Update(Item item, bool isAvailable);
}