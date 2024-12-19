using System.ComponentModel.DataAnnotations;

namespace ShepardPies.Models;


public class Cheese
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    
}