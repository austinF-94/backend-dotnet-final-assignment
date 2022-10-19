using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Posts 
{
    public int PostId { get; set; }
    [Required]

    public string? Body { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }

   //  public DateTime CreatedOn { get; set; }
}