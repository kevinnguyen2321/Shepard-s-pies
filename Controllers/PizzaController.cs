using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepardPies.Data;
using Microsoft.EntityFrameworkCore;
using ShepardPies.Models;
using ShepardPies.Models.DTOs;

namespace ShepardPies.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private ShepardPiesDbContext _dbContext;

    public PizzaController(ShepardPiesDbContext context)
    {
        _dbContext = context;
    }


    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdatePizza(int id, UpdatePizzaDTO pizzaDTO)
    {
        Pizza pizzaToUpdate = _dbContext.Pizzas
        .Include(p => p.Toppings)
        .FirstOrDefault(p => p.Id == id);

        if (pizzaToUpdate == null)
        {
            return NotFound();
        }

        
        pizzaToUpdate.Size = pizzaDTO.Size;
        pizzaToUpdate.Price = pizzaDTO.Price;
        pizzaToUpdate.CheeseId = pizzaDTO.CheeseId;
        pizzaToUpdate.SauceId = pizzaDTO.SauceId;
        pizzaToUpdate.OrderId = pizzaDTO.OrderId;

      _dbContext.PizzaToppings.RemoveRange(pizzaToUpdate.Toppings);

         // Add new toppings if provided
        if (pizzaDTO.ToppingIds != null && pizzaDTO.ToppingIds.Any())
        {
            foreach (int toppingId in pizzaDTO.ToppingIds)
            {
                PizzaTopping newTopping = new PizzaTopping
                {
                    PizzaId = pizzaToUpdate.Id,
                    ToppingId = toppingId
                };

                _dbContext.PizzaToppings.Add(newTopping);
            }
        }

         _dbContext.SaveChanges();

         return NoContent();
    }


    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetPizzaById(int id)
    {
        Pizza foundPizza = _dbContext.Pizzas
        .Include(p => p.Cheese)
        .Include(p => p.Sauce)
        .Include(p => p.Toppings)
        .ThenInclude(pt => pt.Topping)
        .FirstOrDefault(p => p.Id == id);

        if (foundPizza == null)
        {
            return NotFound();
        }

        PizzaDTO pizzaDTO = new PizzaDTO
        {
            Id = foundPizza.Id,
            Size = foundPizza.Size,
            Price = foundPizza.Price,
            CheeseId = foundPizza.CheeseId,
            Cheese = new CheeseDTO
            {
                Id = foundPizza.Cheese.Id,
                Name = foundPizza.Cheese.Name,
               
            },
            SauceId = foundPizza.SauceId,
            Sauce = new SauceDTO
            {
                Id = foundPizza.Sauce.Id,
                Name = foundPizza.Sauce.Name,
                
            },
            OrderId = foundPizza.OrderId,
            Toppings = foundPizza.Toppings != null?
             foundPizza.Toppings.Select(pt => new PizzaToppingDTO
            {
                Id = pt.Id,
                PizzaId = pt.PizzaId,
                ToppingId = pt.ToppingId,
                Topping = new ToppingDTO
                {
                    Id = pt.Topping.Id,
                    Name = pt.Topping.Name,
                    Price = pt.Topping.Price
                    
                }
            }).ToList() : null
        };

        return Ok(pizzaDTO);
    }



    

}