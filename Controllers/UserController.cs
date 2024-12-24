
using Microsoft.AspNetCore.Mvc;
using ShepherdPie.Data;
using ShepherdPie.Models;
using System.Linq;

namespace ShepherdPie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ShepherdPieDbContext _dbContext;

        public UserController(ShepherdPieDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _dbContext.Users.Select(u => new 
            {
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.UserName
            }).ToList();

            return Ok(users);
        }
    }
}
