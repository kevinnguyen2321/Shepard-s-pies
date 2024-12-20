using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepardPies.Data;
using Microsoft.EntityFrameworkCore;
using ShepardPies.Models;
using ShepardPies.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class SauceController: ControllerBase
{
    private ShepardPiesDbContext _dbContext;

    public SauceController(ShepardPiesDbContext context)
    {
        _dbContext = context;
    }



    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
      List<SauceDTO> sauceDTOs = _dbContext.Sauces
       .Select(t => new SauceDTO
        {
            Id = t.Id,
            Name = t.Name,
        }).ToList();

        return Ok(sauceDTOs);
    }
}