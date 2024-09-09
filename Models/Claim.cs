namespace PROG6212_POE_CMCS.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }  // Foreign Key to Lecturer
        public Lecturer Lecturer { get; set; }
        public decimal HoursWorked { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ClaimStatus Status { get; set; }
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}