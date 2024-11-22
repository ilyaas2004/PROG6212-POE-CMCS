using Microsoft.AspNetCore.Identity;

namespace PROG6212_POE_CMCS.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FullName { get; set; }
    }
}
