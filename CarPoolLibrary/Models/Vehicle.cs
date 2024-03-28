using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Vehicle
{
    [Key]
    [Required]
    [Display(Name = "Vehicle Id")]
    public int VehicleId { get; set; }

    [Required]
    public string? Model { get; set; }

    [Required]
    public string? Make { get; set; }

    [Required]
    [Range(1886, 9999, ErrorMessage = "Please enter a valid year")]
    public int? Year { get; set; }

    [Required]
    [Display(Name = "Number of Seats")]
    [Range(1, 7, ErrorMessage = "Please enter a valid number of seats")]
    public int? NumberOfSeats { get; set; }

    [Display(Name = "Vehicle Type")]
    public string? VehicleType { get; set; }

    [Display(Name = "Member Id")]
    public int? MemberId { get; set; } // Driver

    public DateTime? Created { get; set; } = DateTime.Now;

    public DateTime? Modified { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = "System";

    public string ModifiedBy { get; set; } = "System";

    [ForeignKey("MemberId")]
    public Member? Member { get; set; }
}