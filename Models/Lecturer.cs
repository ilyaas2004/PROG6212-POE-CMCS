using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG6212_POE_CMCS.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerID { get; set; }

        [Required(ErrorMessage = "Lecturer name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hourly rate is required.")]
        [Column(TypeName = "decimal(18,2)")] // Explicitly define precision and scale
        public decimal HourlyRate { get; set; }
    }
}
