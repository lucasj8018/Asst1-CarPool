using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Manifest
{
    [Key, Column(Order = 0)]
    [Required]
    public int ManifestId { get; set; }

    [Key, Column(Order = 1)]
    [Required]
    public int MemberId { get; set; }

    public int? TripId { get; set; }

    public string? Notes { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    [ForeignKey("MemberId")]
    public Member? Member { get; set; }
}
