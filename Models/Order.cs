namespace ShepherdPie.Models;
using System.ComponentModel.DataAnnotations.Schema;
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int? OrderTakerEmployeeId { get; set; }
    public int? DeliveryPersonEmployeeId { get; set; }
    public string? UserId { get; set; } // Foreign Key for User
    public string OrderStatus { get; set; } // e.g., Pending, Completed, Cancelled
    public decimal? TipLeftCustomer { get; set; }
    public decimal DeliveryFee { get; set; }

    // Navigation properties
    public User User { get; set; } // The user (formerly "customer") that placed the order
    
    [ForeignKey("OrderTakerEmployeeId")]
    public Employee OrderTaker { get; set; }

    [ForeignKey("DeliveryPersonEmployeeId")]
    public Employee? DeliveryPerson { get; set; }
    public ICollection<Pizza> Pizzas { get; set; }
}
