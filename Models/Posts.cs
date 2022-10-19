using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Posts 
{
    public int PostId { get; set; }
    [Required]

    public string? Post { get; set; }
}