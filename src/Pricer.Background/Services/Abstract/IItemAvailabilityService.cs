using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Background.Services.Abstract;

public interface IItemAvailabilityService
{
	ValueTask Update(Item item, bool isAvailable);
}