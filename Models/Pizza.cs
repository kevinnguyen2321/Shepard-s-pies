using System.ComponentModel.DataAnnotations;

namespace ShepardPies.Models;

public class Pizza
{
    public int Id { get; set; }
    [Required]
    public string Size { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CheeseId { get; set; }
    public Cheese Cheese { get; set; }
    [Required]
    public int SauceId { get; set; }
    public Sauce Sauce { get; set; }
    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public List<Topping> Toppings { get; set; } = new List<Topping>();

    public decimal TotalWithToppings 
    {
        get
        {
            decimal pizzaTotal = Price;
           
            foreach (Topping topping in Toppings)
            {

                pizzaTotal += 0.50M;
            }
           

           
           return pizzaTotal;
        }
    }
}