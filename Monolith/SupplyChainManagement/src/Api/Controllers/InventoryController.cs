using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.src.Features.Inventory.Application.DTOs;
using SupplyChainManagement.src.Features.Inventory.Application.Services;
using SupplyChainManagement.src.Inventory.Domain;

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

        [HttpPost("save")]
        public IActionResult SaveStock([FromBody] AddInventoryItemRequest request)
        {
            try
            {
                _service.SaveStock(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "failed");
            }
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
