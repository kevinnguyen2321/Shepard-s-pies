namespace ShepardPies.Models.DTOs;

public class UpdatePizzaDTO
{
    public string Size { get; set; }
    public decimal Price { get; set; }
     public int CheeseId { get; set; }
    public int SauceId { get; set; }
    public int OrderId { get; set; }
    public List<int> ToppingIds { get; set; } = new List<int>();
}