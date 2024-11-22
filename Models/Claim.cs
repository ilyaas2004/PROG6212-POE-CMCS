using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG6212_POE_CMCS.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public int LecturerID { get; set; }

        [ForeignKey("LecturerID")]
        public Lecturer Lecturer { get; set; }  // Foreign Key Relationship to Lecturer

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Hours worked must be greater than zero.")]
        public int HoursWorked { get; set; } // Ensure this property is required and has a minimum value

        [Required]
        public DateTime SubmissionDate { get; set; }

        [Required]
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending; // Set default status to Pending

        public decimal FinalPayment { get; set; }
        public string? DocumentPath { get; set; }  // Nullable document path
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected  
    }
}
