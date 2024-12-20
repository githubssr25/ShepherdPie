namespace ShepherdPie.Models.DTOs;


public class PizzaDto
{
    public int PizzaId { get; set; }
    public string Size { get; set; }
    public decimal BasePrice { get; set; }
    public List<string> Condiments { get; set; }
}
