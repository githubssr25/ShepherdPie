namespace ShepherdPie.Models;

public class Condiment {

    public int CondimentId { get; set; }
public string CondimentName { get; set; }
public string CondimentType { get; set; } // Sauce, Cheese, Topping
public decimal Cost { get; set; }

public ICollection<PizzaCondiment> PizzaCondiments { get; set; }

}