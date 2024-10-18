using System.ComponentModel.DataAnnotations;

namespace PROG6212_POE_CMCS.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerID { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal HourlyRate { get; set; }
    }
}
