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

// var startOfDay = DateTime.UtcNow.Date;
// var endOfDay = startOfDay.AddDays(1);

            var orders = _dbContext.Orders
                .Include(o => o.OrderTaker)
                .Include(o => o.DeliveryPerson)
                .Include(o => o.Pizzas)
                // .Where(o => o.OrderDate.ToUniversalTime() >= startOfDay && o.OrderDate.ToUniversalTime() < endOfDay)
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
                        BasePrice = p.BasePrice,
                        PizzaCondiments = p.PizzaCondiments.Where(pc => pc.PizzaId == p.PizzaId)
                        .Select(pc1 => new {
                            PizzaId = pc1.PizzaId,
                            CondimentId = pc1.CondimentId,
                            CondimentName = _dbContext.Condiments.FirstOrDefault(cond => cond.CondimentId == pc1.CondimentId).CondimentName,
                            CondimentType = _dbContext.Condiments.FirstOrDefault(cond => cond.CondimentId == pc1.CondimentId).CondimentType,
                        })
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

            var condimentDictionary = _dbContext.Condiments
            .ToDictionary(c => c.CondimentId);
            //what this dictionary is now 
//             {
//     1: { CondimentId: 1, CondimentName: "Pepperoni", CondimentType: "Meat", Cost: 1.75 },
//     2: { CondimentId: 2, CondimentName: "Buffalo Mozzarella", CondimentType: "Cheese", Cost: 1.25 },
//     3: { CondimentId: 3, CondimentName: "Parmesan", CondimentType: "Cheese", Cost: 1.50 },
//     4: { CondimentId: 4, CondimentName: "Mushroom", CondimentType: "Vegetable", Cost: 0.75 },
// }


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
                    Condiments = p.PizzaCondiments.Select(pc => new
                    {
                        CondimentId = pc.CondimentId,
                        CondimentName = condimentDictionary[pc.CondimentId].CondimentName,
                        CondimentType = condimentDictionary[pc.CondimentId].CondimentType
                    }).ToList()
                }).ToList()
            };

            return Ok(orderDTO);
        }

        [HttpGet("by-date")]
        public IActionResult GetByDate([FromQuery] DateTime date)
        {
            var startOfDay = date.Date.ToUniversalTime();
            var endOfDay = startOfDay.AddDays(1);

            Console.WriteLine($"Received Date: {date}");
            var order = _dbContext.Orders
               .Include(o => o.Pizzas)
               .Include(o => o.OrderTaker)
               .Include(o => o.DeliveryPerson)
               .Where(o => o.OrderDate >= startOfDay && o.OrderDate < endOfDay)
               .OrderByDescending(o => o.OrderDate.Date)
               .ToList();

            // date.Date: Converts date into 2024-12-15 00:00:00.
            // date.Date.AddDays(1): Marks the end of the day (2024-12-16 00:00:00).
            // Ensures all orders with a time from 00:00:00 to 23:59:59 are included.

            return Ok(order);

        }

public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
{
    if (createOrderDto == null || !ModelState.IsValid)
    {
        return BadRequest("Invalid order data.");
    }

    // Create the Order object with cascading inserts
    var ourOrder = new Order
    {
        OrderDate = createOrderDto.OrderDate,
        TotalAmount = createOrderDto.TotalAmount,
        OrderTakerEmployeeId = createOrderDto.OrderTakerEmployeeId,
        DeliveryPersonEmployeeId = createOrderDto.DeliveryPersonEmployeeId,
        UserId = createOrderDto.UserId,
        OrderStatus = "Pending",
        TipLeftCustomer = createOrderDto.TipLeftCustomer,
        DeliveryFee = createOrderDto.DeliveryFee,
        Pizzas = createOrderDto.Pizzas.Select(dtoPizza => new Pizza
        {
            Size = dtoPizza.Size,
            BasePrice = dtoPizza.BasePrice,
            PizzaCondiments = dtoPizza.CondimentIds.Select(condimentId => new PizzaCondiment
            {
                CondimentId = condimentId
            }).ToList()
        }).ToList()
    };

    // Add the Order to the database context
    _dbContext.Orders.Add(ourOrder);

    // Save changes to persist the data
    await _dbContext.SaveChangesAsync();

   // Return success response
    return Ok(ourOrder);
}



[HttpPut("{orderId}/pizzas/{pizzaId}")]
public IActionResult UpdatePizza(int orderId, int pizzaId, [FromBody] UpdatePizzaDto updatedPizza)
{
     var order = _dbContext.Orders
    .Include(o => o.Pizzas)
    .ThenInclude(p => p.PizzaCondiments)
    .FirstOrDefault(o => o.OrderId == orderId);

    if (order == null)
    {
        return NotFound("Order not found.");
    }

     // Find the pizza within the order
    var pizza = order.Pizzas.FirstOrDefault(p => p.PizzaId == pizzaId);

    if (pizza == null)
    {
        return NotFound("Pizza not found.");
    }


//old total cost
var previousPizzaBasePrice = pizza.BasePrice;
var previousPizzaCondimentCost= pizza.PizzaCondiments.Sum(pc => 
_dbContext.Condiments.First(c => c.CondimentId == pc.CondimentId).Cost);

var previousPizzaTotal = previousPizzaBasePrice + previousPizzaCondimentCost;

//  pizza.PizzaCondiments: Collection of condiments for a pizza.
// pc: Each condiment in pizza.PizzaCondiments.
// _dbContext.Condiments.First(...): Finds the specific condiment by CondimentId in the Condiments table.
// .Cost: Retrieves the cost of the matched condiment.
// This sums up the costs of all condiments for the pizza.

    pizza.Size = updatedPizza.Size;


    pizza.PizzaCondiments.Clear();// Clear existing condiments

    foreach(var pc in updatedPizza.CondimentIds)
    {
        pizza.PizzaCondiments.Add(new PizzaCondiment
        { 
            PizzaId = pizza.PizzaId,
            CondimentId = pc
        });
    }

//Step 3: Calculate the new base price of the pizza based on the updated size
var newPizzaBasePrice = _dbContext.Pizzas.Where(p => p.Size == updatedPizza.Size)
                                         .Select(pizzaSize => pizzaSize.BasePrice)
                                         .FirstOrDefault();     
//No, you cannot directly use .BasePrice after .Where() because the .Where() method returns a collection (IQueryable<Pizza>), even if it contains only one item. 
//This means the result is still a sequence, not a single object, and you can't directly access a property like .BasePrice on the sequence.


// If no matching size is found, handle the case (e.g., set to 0 or log an error)
if (newPizzaBasePrice == 0)
{
    // Handle the edge case where the size is invalid
    throw new Exception($"Base price for size '{updatedPizza.Size}' not found in database.");
}

var newPizzaCondimentCost = updatedPizza.CondimentIds.Sum(condimentId => _dbContext.Condiments.First(c => c.CondimentId == condimentId).Cost);
// so you dont have to take the sum of condimentID directly you can just take the sum of whatever this is _dbContext.Condiments.First(c => c.CondimentId == condimentId).Cost) 


// Step 3.2: Calculate the total cost of the pizza
var newPizzaTotal = newPizzaBasePrice + newPizzaCondimentCost;

pizza.Size = updatedPizza.Size;
pizza.BasePrice = newPizzaBasePrice;

  // Update order total
    order.TotalAmount = order.TotalAmount - previousPizzaTotal + newPizzaTotal;

_dbContext.SaveChanges();


 var response = new
    {
        UpdatedPizza = new
        {
            pizza.PizzaId,
            pizza.Size,
            pizza.BasePrice,
            Condiments = pizza.PizzaCondiments.Select(pc => new 
            {
                pc.CondimentId,
                pc.Condiment.CondimentName
            })
        },
        NewOrderTotal = order.TotalAmount
    };
    return Ok(response);
}

    }

}

//12-25 1001 for tomorrow we have to actuall ytest to see if this update for pizza within order works both front end an backend 
//also based off what we return we have to figure out proper structure ofw hat to use out of that to actually update the state of hte updated pizza within the list of orders component 
//where the edit pizza was iniitally called have to do the use navigate and what not 
    

