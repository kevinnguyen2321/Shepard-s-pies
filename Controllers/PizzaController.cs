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

    // [HttpGet]
    // [Authorize]
    // public IActionResult Get()
    // {
    //    List<Pizza> pizzas = _dbContext.Pizzas
                            
        
    // }

    
    // [HttpGet("{id}")]
    // [Authorize]
    // public IActionResult GetById(int id)
    // {
  
    // }


    

}