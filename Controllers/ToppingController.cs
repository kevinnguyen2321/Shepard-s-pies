using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepardPies.Data;
using Microsoft.EntityFrameworkCore;
using ShepardPies.Models;
using ShepardPies.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ToppingController: ControllerBase
{
    private ShepardPiesDbContext _dbContext;

    public ToppingController(ShepardPiesDbContext context)
    {
        _dbContext = context;
    }



    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
      List<ToppingDTO> toppingDTOs = _dbContext.Toppings
       .Select(t => new ToppingDTO
        {
            Id = t.Id,
            Name = t.Name,
            Price = t.Price
        }).ToList();

        return Ok(toppingDTOs);
    }
}