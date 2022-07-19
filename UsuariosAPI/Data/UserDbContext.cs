using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UsuariosAPI.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;
        public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            IdentityUser<int> admin = new IdentityUser<int>
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 999
            };

            PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();
            string passwordFromSecret = _configuration.GetValue<string>("AdminInformation:Password");
            admin.PasswordHash = hasher.HashPassword(admin, passwordFromSecret);
            builder.Entity<IdentityUser<int>>().HasData(admin);
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 999,
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 998,
                Name = "regular",
                NormalizedName = "REGULAR"
            });
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 999,
                UserId = 999
            });
        }
    }
}