using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepardPies.Data;
using Microsoft.EntityFrameworkCore;
using ShepardPies.Models;
using ShepardPies.Models.DTOs;

namespace ShepardPies.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private ShepardPiesDbContext _dbContext;

    public OrderController(ShepardPiesDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    // [Authorize]
    public IActionResult Get()
    {
        DateTime today = DateTime.Today;
        List<Order> orders = _dbContext.Orders
            .Where(o => o.OrderPlacedOn.Date == today)
            .OrderByDescending(o => o.OrderPlacedOn)
            .ToList();

        if (orders.Count == 0)
        {
        // Optionally include fallback to show all orders
        orders = _dbContext.Orders
            .OrderByDescending(o => o.OrderPlacedOn)
            .ToList();

            if (orders.Count == 0)
            {
                return Ok(new { message = "No orders found." });
            }

        return Ok(new { message = "No orders found for today. Showing all orders.", orders });
        }

        return Ok(orders);
    }


    
    // [HttpGet("{id}")]
    // [Authorize]
    // public IActionResult GetById(int id)
    // {
  
    // }


    

}

