using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShepardPies.Models;


public class Order
{
    public int Id { get; set; }
    [Required]
    public DateTime OrderPlacedOn { get; set; }
    [Required]
    public int UserProfileId { get; set; }
    [ForeignKey("UserProfileId")]
    public UserProfile OrderTaker { get; set; } 
    public int? DriverId { get; set; }
    [ForeignKey("DriverId")]
    public UserProfile Driver { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Tip must be non-negative.")]
    public decimal? Tip { get; set; }
    public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

     public decimal TotalPrice
    {
        get
        {
            decimal total = 0M;

            foreach (Pizza pizza in Pizzas)
            {
                total += pizza.TotalWithToppings;
                
            }
            
            //Add driver fee if there's a driver
            if (DriverId != null) 
            {
                total += 5.00M; //Delivery fee
                
            }
            
            //Check if there is a tip
            if (Tip.HasValue) 
            {
                total += Tip.Value; //Tip
                
            }
            
            return total;

        }
    } 
}