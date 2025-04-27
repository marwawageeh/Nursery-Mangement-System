using Microsoft.AspNetCore.Identity;

namespace mvc_project.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? Address { get; set; }
    }
}
