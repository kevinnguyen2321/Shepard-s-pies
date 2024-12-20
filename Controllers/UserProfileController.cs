using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepardPies.Data;
using Microsoft.EntityFrameworkCore;
using ShepardPies.Models;
using ShepardPies.Models.DTOs;


[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private ShepardPiesDbContext _dbContext;

    public UserProfileController(ShepardPiesDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
      List<UserProfileDTO> userProfileDTOs = _dbContext.UserProfiles
       .Select(up => new UserProfileDTO
        {
            Id = up.Id,
            FirstName = up.FirstName,
            LastName = up.LastName,
            Address = up.Address,
            IdentityUserId = up.IdentityUserId,
        }).ToList();

        return Ok(userProfileDTOs);
    }

}