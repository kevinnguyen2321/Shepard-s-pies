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
}