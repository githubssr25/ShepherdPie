namespace ShepherdPie.Models.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; }
    public string CustomerName { get; set; }
    public string OrderTakerName { get; set; }
    public string DeliveryPersonName { get; set; }
}
