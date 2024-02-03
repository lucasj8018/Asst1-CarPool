using System.ComponentModel.DataAnnotations;

namespace CarPoolLibrary.Models;
public class Member
{
    [Key]
    [Required]
    public int MemberId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }
}
