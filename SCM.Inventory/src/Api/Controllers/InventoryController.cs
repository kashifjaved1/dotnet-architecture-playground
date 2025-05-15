using Microsoft.AspNetCore.Mvc;
using SCM.Inventory.src.Infrastructure;

namespace SCM.Inventory.src.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDbContext _db;

        [HttpPost("allocate")]
        public IActionResult Allocate([FromBody] AllocationRequest request)
        {
            var item = _db.Items.Find(request.ItemId);
            item.Allocate(request.Quantity);
            _db.SaveChanges();
            return Ok();
        }
    }
}
