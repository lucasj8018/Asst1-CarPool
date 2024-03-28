using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Vehicle
{
    [Key]
    [Required]
    public int VehicleId { get; set; }

    [Required]
    public string? Model { get; set; }

    [Required]
    public string? Make { get; set; }

    [Required]
    [Range(1886, 9999, ErrorMessage = "Please enter a valid year")]
    public int? Year { get; set; }

    [Required]
    [Range(1, 7, ErrorMessage = "Please enter a valid number of seats")]
    public int? NumberOfSeats { get; set; }

    public string? VehicleType { get; set; }

    public int? MemberId { get; set; }

    public DateTime? Created { get; set; } = DateTime.Now;

    public DateTime? Modified { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = "System";

    public string ModifiedBy { get; set; } = "System";

    [ForeignKey("MemberId")]
    public Member? Member { get; set; }
}