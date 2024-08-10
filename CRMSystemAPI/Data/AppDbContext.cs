using CRMSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRMSystemAPI.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            int ADMIN_ID = 1;
            int ADMIN_ROLE_ID = 1;
            int CLIENT_ROLE_ID = 2;

            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(iul => new { iul.LoginProvider, iul.ProviderKey, iul.UserId });
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(iut => new { iut.UserId, iut.LoginProvider, iut.Name });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                    new IdentityRole<int>
                    {
                        Name = "Admin",
                        Id = ADMIN_ROLE_ID,
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
                    },
                    new IdentityRole<int>
                    {
                        Name = "Client",
                        Id = CLIENT_ROLE_ID,
                        NormalizedName = "CLIENT",
                        ConcurrencyStamp = CLIENT_ROLE_ID.ToString()
                    }
            );

            var appUser = new User
            {
                Id = ADMIN_ID,
                Email = "admin@email.com",
                Name = "Admin",
                UserName = "admin",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                NormalizedUserName = "ADMIN"
            };

            PasswordHasher<User> ph = new PasswordHasher<User>();
            appUser.PasswordHash = ph.HashPassword(appUser, "admin123");

            modelBuilder.Entity<User>().HasData(appUser);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
