using Microsoft.AspNetCore.Identity;
using PMS.Web.Entities;

namespace PMS.Web.Services
{
    public static class SeedData
    {
        public static void Seed1(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUser(userManager);
        }
        public static void SeedUser(UserManager<AppUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };
                var result = userManager.CreateAsync(user, "P@ssword123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Patient").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Patient"
                };
                roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Physician").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Physician"
                };
                roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Nurse").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Nurse"
                };
                roleManager.CreateAsync(role);
            }
        }
    }
}
