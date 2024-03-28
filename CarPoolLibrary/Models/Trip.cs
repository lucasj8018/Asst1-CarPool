using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Trip
{
    [Key]
    [Required]
    public int TripId { get; set; }

    [Required]
    public int VehicleId { get; set; }

    [Required]
    public DateOnly? Date { get; set; }

    [Required]
    public TimeOnly? Time { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Destination Adress")]
    public string? Destination { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Meeting Address")]
    public string? MeetingAddress { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime Modified { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = "System";

    public string ModifiedBy { get; set; } = "System";

    [ForeignKey("VehicleId")]
    public Vehicle? Vehicle { get; set; }

    // Stores the passengers of the trips the member is a driver for
    public List<Member>? Members { get; set; } = [];

}