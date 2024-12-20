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




    

}