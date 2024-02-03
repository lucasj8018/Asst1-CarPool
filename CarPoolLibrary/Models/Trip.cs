using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Trip
{
    [Key]
    [Required]
    public int TripId { get; set; }

    [Key]
    [Required]
    public string? VehicleId { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Destination { get; set; }

    public string? MeetingAddress { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    [ForeignKey("VehicleId")]
    public Vehicle? Vehicle { get; set; }

}