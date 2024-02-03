using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Vehicle
{
    [Key]
    [Required]
    public int VehicleId { get; set; }

    public string? Model { get; set; }

    public string? Make { get; set; }

    public int? Year { get; set; }

    public int? NumberOfSeats { get; set; }

    public string? VehicleType { get; set; }

    public int? MemberId { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    [ForeignKey("MemberId")]
    public Member? Member { get; set; }
}