﻿using Microsoft.AspNetCore.Identity;

namespace CRMSystemAPI.Models.DatabaseModels
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
