using CRUD_TaskInHome26.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_TaskInHome26.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly List<Order> Orders =
    [
        new Order { Id = 1, Number = "ORD-1001", Total = 125.50m, CreatedAt = new DateTime(2026, 1, 15) },
        new Order { Id = 2, Number = "ORD-1002", Total = 480.00m, CreatedAt = new DateTime(2026, 2, 10) },
        new Order { Id = 3, Number = "ORD-1003", Total = 75.25m, CreatedAt = new DateTime(2026, 3, 5) }
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAll()
    {
        return Ok(Orders);
    }

    [HttpGet("search")]
    public ActionResult<IEnumerable<Order>> Search([FromQuery] string? number, [FromQuery] decimal? minTotal)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            return BadRequest("Query parameter 'number' is required.");
        }

        var results = Orders
            .Where(order => order.Number.Contains(number, StringComparison.OrdinalIgnoreCase))
            .Where(order => !minTotal.HasValue || order.Total >= minTotal.Value)
            .ToList();

        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = Orders.FirstOrDefault(order => order.Id == id);
        return order is null ? NotFound("Order not found") : Ok(order);
    }

    [HttpPost]
    public ActionResult<Order> Create(OrderRequest request)
    {
        var order = new Order
        {
            Id = Orders.Count == 0 ? 1 : Orders.Max(order => order.Id) + 1,
            Number = request.Number!.Trim(),
            Total = request.Total!.Value,
            CreatedAt = DateTime.UtcNow
        };

        Orders.Add(order);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Order> Update(int id, OrderRequest request)
    {
        var order = Orders.FirstOrDefault(order => order.Id == id);
        if (order is null)
        {
            return NotFound("Order not found");
        }

        order.Number = request.Number!.Trim();
        order.Total = request.Total!.Value;

        return Ok(order);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var order = Orders.FirstOrDefault(order => order.Id == id);
        if (order is null)
        {
            return NotFound("Order not found");
        }

        Orders.Remove(order);
        return NoContent();
    }
}
