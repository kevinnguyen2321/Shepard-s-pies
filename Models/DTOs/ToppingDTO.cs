namespace ShepardPies.Models.DTOs;


public class ToppingDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int PizzaId { get; set; }
    
}