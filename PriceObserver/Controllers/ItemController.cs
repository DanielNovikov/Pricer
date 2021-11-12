using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("grouped/{userId:long}")]
        public async Task<IActionResult> GetGroupedByUserId(long userId)
        {
            var result = await _itemService.GetGroupedByUserId(userId);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.Delete(id);
            return NoContent();
        }
    }
}