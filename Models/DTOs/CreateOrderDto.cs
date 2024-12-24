namespace ShepherdPie.Models.DTOs;

public class CreateOrderDto
{
    public string UserId { get; set; }                     // Matches frontend's `chosenUser`
    public int? OrderTakerEmployeeId { get; set; }         // Matches `chosenOrderTaker`
    public int? DeliveryPersonEmployeeId { get; set; }     // Matches `chosenDeliveryPerson`
    public decimal TotalAmount { get; set; }              // Matches `finalTotalAmount`
    public decimal TipLeftCustomer { get; set; }          // Matches `enteredTip`
    public decimal DeliveryFee { get; set; }              // Matches `deliveryFee`
    public DateTime OrderDate { get; set; }               // Matches `new Date().toISOString()`
    public List<CreatePizzaDTO> Pizzas { get; set; }      // List of pizzas being sent
}
