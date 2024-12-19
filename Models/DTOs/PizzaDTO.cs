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
    public List<ToppingDTO> Toppings { get; set; } = new List<ToppingDTO>();
    public decimal TotalWithToppings 
    {
        get
        {
            decimal pizzaTotal = Price;
           
            foreach (ToppingDTO topping in Toppings)
            {

                pizzaTotal += 0.50M;
            }
           

           
           return pizzaTotal;
        }
    }

}