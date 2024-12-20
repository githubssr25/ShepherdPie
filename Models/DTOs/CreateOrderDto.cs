namespace ShepherdPie.Models.DTOs;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public int? OrderTakerEmployeeId { get; set; }
    public int? DeliveryPersonEmployeeId { get; set; }
    public List<PizzaDto> Pizzas { get; set; }
}
