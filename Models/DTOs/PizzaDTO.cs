namespace ShepardPies.Models.DTOs;


public class PizzaDTO
{
    public int Id { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public int CheeseId { get; set; }
    public CheeseDTO Cheese { get; set; }
    public int SauceId { get; set; }
    public SauceDTO Sauce { get; set; }
    public int OrderId { get; set; }
    public OrderDTO Order { get; set; }
    public List<PizzaToppingDTO> Toppings { get; set; } = new List<PizzaToppingDTO>();
    public decimal TotalWithToppings 
    {
        get
        {
            decimal pizzaTotal = Price;
           
            foreach (PizzaToppingDTO topping in Toppings)
            {

                pizzaTotal += 0.50M;
            }
           

           
           return pizzaTotal;
        }
    }

}