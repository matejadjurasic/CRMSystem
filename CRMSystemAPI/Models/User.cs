﻿using Microsoft.AspNetCore.Identity;

namespace CRMSystemAPI.Models
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
