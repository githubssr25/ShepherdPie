

using Microsoft.AspNetCore.Mvc;
using ShepherdPie.Data;
using ShepherdPie.Models;
using System.Linq;

namespace ShepherdPie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CondimentController : ControllerBase
    {
        private readonly ShepherdPieDbContext _dbContext;

        public CondimentController(ShepherdPieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// GET /api/condiment
        /// Fetch all condiments
        /// </summary>
        /// <returns>List of condiments</returns>
        [HttpGet]
        public IActionResult GetAllCondiments()
        {
            var condiments = _dbContext.Condiments.ToList();
            return Ok(condiments);
        }
    }
}
