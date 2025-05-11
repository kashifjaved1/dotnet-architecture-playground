using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.src.Inventory.Application.DTOs;
using SupplyChainManagement.src.Inventory.Application.Services;

namespace SupplyChainManagement.src.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _service;

        public InventoryController(InventoryService service)
        {
            _service = service;
        }

        [HttpPost("allocate")]
        public IActionResult AllocateStock([FromBody] AllocationRequest request)
        {
            try
            {
                _service.AllocateStock(request.ItemId, request.Quantity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
