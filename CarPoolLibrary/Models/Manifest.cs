using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Manifest
{
    [Key, Column(Order = 0)]
    [Required]
    public int ManifestId { get; set; }

    [Column(Order = 1)]
    [Required]
    public int MemberId { get; set; }

    public int? TripId { get; set; }

    public string? Notes { get; set; }

    public DateTime? Created { get; set; } = DateTime.Now;

    public DateTime? Modified { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; } = "System";

    public string? ModifiedBy { get; set; } = "System";

    [ForeignKey("MemberId")]
    public Member? Member { get; set; }
}
