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
    [Authorize]
    public IActionResult Get()
    {
        DateTime today = DateTime.Today;
        List<Order> orders = _dbContext.Orders
            .Where(o => o.OrderPlacedOn.Date == today)
            .Include(o => o.OrderTaker)
            .Include(o => o.Driver)
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Toppings)
            .OrderByDescending(o => o.OrderPlacedOn)
            .ToList();

            if (orders.Count == 0)
            {
                orders = _dbContext.Orders
                    .Include(o => o.OrderTaker)
                    .Include(o => o.Driver)
                    .Include(o => o.Pizzas)
                    .ThenInclude(p => p.Toppings)
                    .OrderByDescending(o => o.OrderPlacedOn)
                    .ToList();
                
            }

            return Ok(orders);
    }


    
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        Order foundOrder = _dbContext.Orders
            .Include(o => o.OrderTaker)
            .Include(o => o.Driver)
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Sauce)
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Cheese)
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Toppings)
            .FirstOrDefault(o => o.Id == id);

            if (foundOrder == null)
            {
                return NotFound();
            }

            return Ok(new OrderDTO
            {
                Id = foundOrder.Id,
                OrderPlacedOn = foundOrder.OrderPlacedOn,
                UserProfileId = foundOrder.UserProfileId,
                OrderTaker = new UserProfileDTO
                {
                    Id = foundOrder.OrderTaker.Id,
                    FirstName = foundOrder.OrderTaker.FirstName,
                    LastName = foundOrder.OrderTaker.LastName,
                },
                DriverId = foundOrder.DriverId,
                Driver = foundOrder.Driver == null ? null : new UserProfileDTO
                {
                    Id = foundOrder.Driver.Id,
                    FirstName = foundOrder.Driver.FirstName,
                    LastName = foundOrder.Driver.LastName,
                },
                Tip = foundOrder.Tip != null ? foundOrder.Tip : null,
                Pizzas = foundOrder.Pizzas != null ? foundOrder.Pizzas
                .Select(p => new PizzaDTO
                {
                    Id = p.Id,
                    Size = p.Size,
                    Price = p.Price,
                    SauceId = p.SauceId,
                    Sauce = p.Sauce != null ? new SauceDTO
                    {
                        Id = p.Sauce.Id,
                        Name = p.Sauce.Name,
                    }:null,
                    CheeseId = p.CheeseId,
                    Cheese = p.Cheese != null ? new CheeseDTO
                    {
                        Id = p.Cheese.Id,
                        Name = p.Cheese.Name,
                    }:null,
                    Toppings = p.Toppings != null ?  p.Toppings
                    .Select(t => new ToppingDTO
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Price = t.Price,
                    }).ToList():new List<ToppingDTO>(),
                
                }).ToList():new List<PizzaDTO>()


            });
  
    }


    

}

