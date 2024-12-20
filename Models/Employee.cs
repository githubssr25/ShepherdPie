namespace ShepherdPie.Models;


public class Employee{

    public int EmployeeId { get; set; }
public string Name { get; set; }

public ICollection<Order> TakenOrders { get; set; }
public ICollection<Order> DeliveredOrders { get; set; }

}