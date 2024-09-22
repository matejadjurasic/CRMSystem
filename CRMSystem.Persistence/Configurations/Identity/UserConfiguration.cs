using CRMSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Persistence.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var admin = new User
            {
                Id = 1,
                Email = "admin@email.com",
                Name = "Admin",
                UserName = "admin",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                NormalizedUserName = "ADMIN"
            };

            var client = new User
            {
                Id = 2,
                Email = "client@email.com",
                Name = "Client",
                UserName = "client",
                NormalizedEmail = "CLIENT@EMAIL.COM",
                NormalizedUserName = "CLIENT"
            };

            PasswordHasher<User> ph = new PasswordHasher<User>();
            admin.PasswordHash = ph.HashPassword(admin, "admin123");
            client.PasswordHash = ph.HashPassword(client, "client123");

            

            builder.HasData(
                 admin,client
             );
        }
    }
}
