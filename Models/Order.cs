namespace ShepherdPie.Models;
using System.ComponentModel.DataAnnotations.Schema;
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int? OrderTakerEmployeeId { get; set; }
    public int? DeliveryPersonEmployeeId { get; set; }

    public string UserId { get; set;} = string.Empty; // Match IdentityUser key type
    public string? OrderStatus { get; set; } // e.g., Pending, Completed, Cancelled
    public decimal? TipLeftCustomer { get; set; }
    public decimal DeliveryFee { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set;}

    [ForeignKey("OrderTakerEmployeeId")]
    public Employee? OrderTaker { get; set; }

    [ForeignKey("DeliveryPersonEmployeeId")]
    public Employee? DeliveryPerson { get; set; }
    public ICollection<Pizza> Pizzas { get; set; }
}
