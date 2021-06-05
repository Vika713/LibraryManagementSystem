using Domain.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Areas.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Identity");

            base.OnModelCreating(modelBuilder);

            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRoles(modelBuilder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            ApplicationUser user = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin@lms.com",
                NormalizedUserName = "ADMIN@LMS.COM",
                Email = "admin@lms.com",
                NormalizedEmail = "ADMIN@LMS.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                IsEnabled = true,
                PasswordHash = passwordHasher.HashPassword(null, "Admin123.")
            };

            builder.Entity<ApplicationUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = Common.Enumeration.Roles.Admin, ConcurrencyStamp = "1", NormalizedName = Common.Enumeration.Roles.Admin.ToUpper() },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = Common.Enumeration.Roles.Librarian, ConcurrencyStamp = "2", NormalizedName = Common.Enumeration.Roles.Librarian.ToUpper() },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = Common.Enumeration.Roles.Member, ConcurrencyStamp = "3", NormalizedName = Common.Enumeration.Roles.Member.ToUpper() }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                );
        }
    }
}
