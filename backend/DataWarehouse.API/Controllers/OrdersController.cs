using DataWarehouse.API.Models.RecordModels;
using DataWarehouse.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DataWarehouse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderRepository _repository;

    public OrdersController(OrderRepository repository)
    {
        _repository = repository;
    }

    // GET: api/orders
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await _repository.GetOrdersAsync();
        return Ok(orders);
    }

    // POST: api/orders
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Order order)
    {
        // ✅ FIX: Set OrderDate on the server
        order.OrderDate = DateTime.UtcNow;

        await _repository.CreateOrderAsync(order);

        return Ok(order);
    }
}

