using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Manifest
{
    [Key]
    public int ManifestId { get; set; }

    [Required]
    public int MemberId { get; set; }

    [Required]
    public int? TripId { get; set; }
    public string? DestinationAddress { get; set; }


    public string? Notes { get; set; }

    public DateTime? Created { get; set; } = DateTime.Now;

    public DateTime? Modified { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; } = "System";

    public string? ModifiedBy { get; set; } = "System";

    [ForeignKey("MemberId")]
    public Member? Member { get; set; }
}
