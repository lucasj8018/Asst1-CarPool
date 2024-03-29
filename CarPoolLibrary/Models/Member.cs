using System.ComponentModel.DataAnnotations;

namespace CarPoolLibrary.Models;
public class Member
{
    [Key]
    public int MemberId { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [EmailAddress]
    [Required]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Invalid mobile number format. Use 123-456-7890.")]
    public string? Mobile { get; set; }

    [Required]
    public string? Street { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid postal code format. Use A1A 1A1.")]
    public string? PostalCode { get; set; }

    [Required]
    public string? Country { get; set; }

    public DateTime? Created { get; set; } = DateTime.Now;

    public DateTime? Modified { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public bool DriverRequest { get; set; } = false;

    // Stores the trips that the member is registered for
    public List<Trip>? Trips { get; } = [];
}
