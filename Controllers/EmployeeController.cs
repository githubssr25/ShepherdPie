
using Microsoft.AspNetCore.Mvc;
using ShepherdPie.Data;
using ShepherdPie.Models;
using System.Linq;

namespace ShepherdPie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ShepherdPieDbContext _dbContext;

        public EmployeeController(ShepherdPieDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _dbContext.Employees.Select(e => new 
            {
                e.EmployeeId,
                e.Name
            }).ToList();

            return Ok(employees);
        }
    }
}
