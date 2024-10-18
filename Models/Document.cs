using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG6212_POE_CMCS.Models
{
    public class Document
    {
        [Key]
        public int DocumentID { get; set; }

        [Required]
        public int ClaimID { get; set; }

        [ForeignKey("ClaimID")]
        public Claim Claim { get; set; }  // Foreign Key to Claim

        [Required]
        public string? FileName { get; set; }

        [Required]
        public string? FileType { get; set; }
    }
}
