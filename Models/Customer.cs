using Microsoft.AspNetCore.Identity;

namespace ShepherdPie.Models;

public class User : IdentityUser // Inherits basic ASP.NET Identity properties
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}