using Microsoft.AspNetCore.Mvc;
using Publisher.Model;
using Publisher.Services;

namespace Publisher.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Order>> Get(string id)
    {
        var order = await _orderService.GetOrder(id);

        if (order is null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Order order)
    {
        try
        {
            _logger.LogInformation("Order received: {Order}", order);
             _orderService.CreateOrder(order);
            return Ok();
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}