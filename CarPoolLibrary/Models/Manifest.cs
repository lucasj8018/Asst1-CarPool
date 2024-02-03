using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Manifest
{
    [Key]
    [Required]
    public int ManifestId { get; set; }

    [Key]
    [Required]
    public int MemberId { get; set; }

    public int? TripId { get; set; }

    public string? Notes { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    [ForeignKey("TripId")]
    public Member? Member { get; set; }

}