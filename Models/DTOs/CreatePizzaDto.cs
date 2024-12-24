

namespace ShepherdPie.Models
{
    public class CreatePizzaDTO
    {
        public string Size { get; set; } = string.Empty; // e.g., Large, Medium, Small
        public decimal BasePrice { get; set; }          // e.g., 15.00, 12.00, 10.00
        public List<int> CondimentIds { get; set; } = new List<int>(); // Selected condiment IDs
    }
}
