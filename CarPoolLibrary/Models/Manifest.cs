using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolLibrary.Models;

public class Manifest
{
    [Key]
    [Display(Name = "Manifest Id")]
    public int ManifestId { get; set; }

    [Required]
    [Display(Name = "Member Id")]
    public int MemberId { get; set; }

    [Required]
    [Display(Name = "Trip Id")]
    public int TripId { get; set; }

    public string? Notes { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime Modified { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = "System";

    public string ModifiedBy { get; set; } = "System";

    [ForeignKey("MemberId")]
    public Member? Member { get; set; }

    [ForeignKey("TripId")]
    public Trip? Trip { get; set; }
}
