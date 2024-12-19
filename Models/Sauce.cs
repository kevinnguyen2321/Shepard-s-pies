using System.ComponentModel.DataAnnotations;

namespace ShepardPies.Models;

public class Sauce
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}