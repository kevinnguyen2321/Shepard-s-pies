using System.ComponentModel.DataAnnotations;

namespace ShepardPies.Models.DTOs;

public class createOrderDTO
{
    [Required]
    public int UserProfileId { get; set; }
    public int? DriverId { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Tip must be non-negative.")]
    public decimal? Tip { get; set; }
   
}