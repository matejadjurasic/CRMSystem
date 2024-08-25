using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CRMSystemAPI.Models.DatabaseModels
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
