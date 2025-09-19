using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HospitalManagement.DataSeeder
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Check if the "Doctor" role exists. If not, create it.
            if (!await roleManager.RoleExistsAsync("Doctor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Doctor"));
            }

            // Check if the "Nurse" role exists. If not, create it.
            if (!await roleManager.RoleExistsAsync("Nurse"))
            {
                await roleManager.CreateAsync(new IdentityRole("Nurse"));
            }
        }
    }
}