using Microsoft.AspNetCore.Mvc;
using OrderService.src.Application.Commands;
using OrderService.src.Application.Handlers;

namespace OrderService.src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly CreateOrderHandler _handler;

    public OrderController(CreateOrderHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _handler.HandleAsync(command);
        return Ok(orderId);
    }
}