
namespace ShepherdPie.Models;

public class PizzaCondiment{

    public int PizzaCondimentId { get; set; }
public int PizzaId { get; set; }
public int CondimentId { get; set; }

public Pizza Pizza { get; set; }
public Condiment Condiment { get; set; }

}