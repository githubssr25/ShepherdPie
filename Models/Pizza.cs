
namespace ShepherdPie.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Pizza {
public int PizzaId { get; set; }

 public int OrderId { get; set; } // This is the foreign key for Order (No [ForeignKey] attribute needed)

public string Size { get; set; } // Small, Medium, Large
public decimal BasePrice { get; set; }

public Order Order { get; set; }
public ICollection<PizzaCondiment> PizzaCondiments { get; set; }



}