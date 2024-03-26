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
    [RegularExpression(@"^(\+[0-9]{1,3})?([0-9]{10})$", ErrorMessage = "Invalid mobile number")]
    public string? Mobile { get; set; }

    [Required]
    public string? Street { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Invalid postal code")]
    public string? PostalCode { get; set; }

    [Required]
    public string? Country { get; set; }

    public DateTime? Created { get; set; } = DateTime.Now;

    public DateTime? Modified { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; } = "System";

    public string? ModifiedBy { get; set; } = "System";

    // Stores the trips that the member is registered for
    public List<Trip>? Trips { get; } = [];
}
