using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models;

public class User 
{
    [JsonIgnore]
    public int UserId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? State { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }

    // public DateTime? CreatedOn { get; set; }

    [JsonIgnore]
    public IEnumerable<Posts>? Posts { get; set; } //ask about syntax
}