using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShepherdPie.Data; // Adjust namespace as needed
using ShepherdPie.Models; // Import your models
using ShepherdPie.Models.DTOs; // If you have DTOs

namespace ShepherdPie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ShepherdPieDbContext _dbContext;
    
        public OrderController(ShepherdPieDbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// GET /api/orders
        /// Get a list of all orders, including order taker, delivery person, and pizza details
        /// </summary>
        /// <returns>List of orders</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var orders = _dbContext.Orders
                .Include(o => o.OrderTaker)
                .Include(o => o.DeliveryPerson)
                .Include(o => o.Pizzas)
                .Where(o => o.OrderDate.Date == DateTime.UtcNow.Date)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new 
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderStatus = o.OrderStatus,
                    TipLeftCustomer = o.TipLeftCustomer,
                    DeliveryFee = o.DeliveryFee,
                    OrderTaker = o.OrderTaker != null ? new 
                    {
                        EmployeeId = o.OrderTaker.EmployeeId,
                        Name = o.OrderTaker.Name
                    } : null,
                    DeliveryPerson = o.DeliveryPerson != null ? new 
                    {
                        EmployeeId = o.DeliveryPerson.EmployeeId,
                        Name = o.DeliveryPerson.Name
                    } : null,
                    Pizzas = o.Pizzas.Select(p => new 
                    {
                        PizzaId = p.PizzaId,
                        Size = p.Size,
                        BasePrice = p.BasePrice
                    }).ToList()
                })
                .ToList();

            return Ok(orders);
        }

        /// <summary>
        /// GET /api/orders/{id}/pizzas
        /// Get a specific order by its ID, along with its associated pizzas
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order with associated pizzas</returns>
        [HttpGet("{id}/pizzas")]
        public IActionResult GetById(int id)
        {
            var order = _dbContext.Orders
                .Include(o => o.Pizzas)
                .Include(o => o.OrderTaker)
                .Include(o => o.DeliveryPerson)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDTO = new 
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                TipLeftCustomer = order.TipLeftCustomer,
                DeliveryFee = order.DeliveryFee,
                OrderTaker = order.OrderTaker != null ? new 
                {
                    EmployeeId = order.OrderTaker.EmployeeId,
                    Name = order.OrderTaker.Name
                } : null,
                DeliveryPerson = order.DeliveryPerson != null ? new 
                {
                    EmployeeId = order.DeliveryPerson.EmployeeId,
                    Name = order.DeliveryPerson.Name
                } : null,
                Pizzas = order.Pizzas.Select(p => new 
                {
                    PizzaId = p.PizzaId,
                    Size = p.Size,
                    BasePrice = p.BasePrice,
                    Condiments = _dbContext.PizzaCondiments
                        .Where(pc => pc.PizzaId == p.PizzaId)
                        .Join(
                            _dbContext.Condiments,
                            pc => pc.CondimentId,
                            c => c.CondimentId,
                            (pc, c) => new 
                            {
                                CondimentId = c.CondimentId,
                                CondimentName = c.CondimentName,
                                CondimentType = c.CondimentType
                            }
                        ).ToList()
                }).ToList()
            };

            return Ok(orderDTO);
        }
    }
}
