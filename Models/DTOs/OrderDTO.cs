using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShepardPies.Models.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime OrderPlacedOn { get; set; }
    public int UserProfileId { get; set; }
    [ForeignKey("UserProfileId")]
    public UserProfileDTO OrderTaker { get; set; }
    public int? DriverId { get; set; }
    [ForeignKey("DriverId")]
    public UserProfileDTO Driver { get; set; } 
    [Range(0, double.MaxValue, ErrorMessage = "Tip must be non-negative.")]
    public decimal? Tip { get; set; }
    public List<PizzaDTO> Pizzas { get; set; } = new List<PizzaDTO>();
    public decimal TotalPrice
    {
        get
        {
            decimal total = 0M;

            foreach (PizzaDTO pizza in Pizzas)
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