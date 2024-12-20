using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepardPies.Data;
using Microsoft.EntityFrameworkCore;
using ShepardPies.Models;
using ShepardPies.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class CheeseController: ControllerBase
{
    private ShepardPiesDbContext _dbContext;

    public CheeseController(ShepardPiesDbContext context)
    {
        _dbContext = context;
    }



    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
      List<CheeseDTO> cheeseDTOs = _dbContext.Cheeses
       .Select(t => new CheeseDTO
        {
            Id = t.Id,
            Name = t.Name,
        }).ToList();

        return Ok(cheeseDTOs);
    }
}