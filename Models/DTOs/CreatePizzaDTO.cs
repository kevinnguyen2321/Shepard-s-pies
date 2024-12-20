namespace ShepardPies.Models.DTOs;

public class CreatePizzaDTO
{
    public string Size { get; set; }
    public decimal Price { get; set; }
    public int CheeseId { get; set; }
    public int SauceId { get; set; }
    public List<int> ToppingIds { get; set; } = new List<int>();
}