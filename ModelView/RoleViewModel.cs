using System.ComponentModel.DataAnnotations;

namespace mvc_project.ModelView
{
    public class RoleViewModel
    {
        [Display(Name = "Role NAme")]
        public string RoleName { get; set; }
    }
}
