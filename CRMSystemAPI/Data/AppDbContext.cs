using CRMSystemAPI.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRMSystemAPI.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            int ADMIN_ID = 1;
            int ADMIN_ROLE_ID = 1;
            int CLIENT_ROLE_ID = 2;


            modelBuilder.Entity<User>(b =>
            {
                b.HasMany(u => u.Projects)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

                b.HasIndex(u => u.Email)
                .IsUnique();
            });


            modelBuilder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });

            modelBuilder.Entity<Role>().HasData(
                    new Role
                    {
                        Name = "Admin",
                        Id = ADMIN_ROLE_ID,
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
                    },
                    new Role
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

            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
