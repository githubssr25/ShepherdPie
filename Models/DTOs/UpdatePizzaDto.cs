namespace ShepherdPie.Models.DTOs;


public class UpdatePizzaDto
{
    public int PizzaId { get; set; }
    public string Size { get; set; }
    public List<int> CondimentIds { get; set; }
}
