using PriceObserver.Data.Persistent.Models;
using System.Threading.Tasks;

namespace PriceObserver.Background.Services.Abstract;

public interface IItemAvailabilityChanger
{
	ValueTask Change(Item item, bool isAvailable);
}