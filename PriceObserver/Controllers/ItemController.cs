using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceObserver.Controllers.Base;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Controllers
{
    public class ItemController : AuthorizedControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(
            IItemService itemService, 
            IUserRepository userRepository) 
            : base(userRepository)
        {
            _itemService = itemService;
        }

        [HttpGet("grouped")]
        public async Task<IActionResult> GetGroupedByUserId()
        {
            var userId = GetUserId();
            var result = await _itemService.GetGroupedByUserId(userId);
            
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            await _itemService.Delete(id, userId);
            
            return NoContent();
        }
    }
}