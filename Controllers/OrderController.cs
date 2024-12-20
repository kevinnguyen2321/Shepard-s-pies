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
            .Include(o => o.OrderTaker)
            .Include(o => o.Driver)
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Toppings)
            .OrderByDescending(o => o.OrderPlacedOn)
            .ToList();

            

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
            .ThenInclude(pt => pt.Topping)
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
                    .Select(t => new PizzaToppingDTO
                    {
                        Id = t.Id,
                       PizzaId = t.PizzaId,
                       Pizza = null,
                       ToppingId = t.ToppingId,
                          Topping = t.Topping != null ? new ToppingDTO
                          {
                            Id = t.Topping.Id,
                            Name = t.Topping.Name,
                            Price = t.Topping.Price,
                          }:null,

                    }).ToList():new List<PizzaToppingDTO>(),
                
                }).ToList():new List<PizzaDTO>()


            });
  
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateNewOrder(createOrderDTO orderDTO) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Order newOrder = new Order
        {
            OrderPlacedOn = DateTime.Now,
            UserProfileId = orderDTO.UserProfileId,
            DriverId = orderDTO.DriverId,
            Tip = orderDTO.Tip,
        };

        _dbContext.Orders.Add(newOrder);
        _dbContext.SaveChanges();

        return Created($"/api/order/{newOrder.Id}", newOrder);

    }

    [HttpPost("{id}/add-pizza")]
    [Authorize]
    public IActionResult AddPizzaWithToppingsToOrder(int id, CreatePizzaDTO pizzaDTO)
    {
        Order foundOrder = _dbContext.Orders
        .FirstOrDefault(o => o.Id == id);

        if (foundOrder == null)
        {
            return NotFound("Order not found");
        }

        


         Pizza newPizza = new Pizza
        {
            Size = pizzaDTO.Size,
            Price = pizzaDTO.Price,
            CheeseId = pizzaDTO.CheeseId,
            SauceId = pizzaDTO.SauceId,
            OrderId = id,
        };

        _dbContext.Pizzas.Add(newPizza);
          _dbContext.SaveChanges(); 
        

        foreach (int toppingId in pizzaDTO.ToppingIds)
        {
            PizzaTopping newPizzaTopping = new PizzaTopping
            {
                PizzaId = newPizza.Id,
                ToppingId = toppingId,
            };

            _dbContext.PizzaToppings.Add(newPizzaTopping);
        }

            _dbContext.SaveChanges();

        PizzaDTO newPizzaDTO = new PizzaDTO
        {
            Id = newPizza.Id,
            Size = newPizza.Size,
            Price = newPizza.Price,
            CheeseId = newPizza.CheeseId,
            SauceId = newPizza.SauceId,
            OrderId = newPizza.OrderId,
            Toppings = newPizza.Toppings.Select(pt => new PizzaToppingDTO
            {
                Id = pt.Id,
                PizzaId = pt.PizzaId,
                ToppingId = pt.ToppingId,
            }).ToList(),
        };

        return Created($"/api/pizza/{id}/{newPizza.Id}", newPizzaDTO);

    }


    

}

