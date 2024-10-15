using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace FormMaster.DAL.DataContext.Seeds;

public class IdentityDataSeeder(UserManager<User> userManager, RoleManager<UserRole> roleManager) :
    IIdentityDataSeeder
{
    public async Task SeedRolesAsync()
    {
        var roleNames = Enum.GetNames<IdentityRoleConstants>();

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new UserRole(roleName));
            }
        }
    }

    public async Task SeedAdminUserAsync()
    {
        var admin = new User()
        {
            UserName = "root",
            Email = "root@localhost"
        };
        var result = await userManager.CreateAsync(admin, "root");

        if (result.Succeeded)
        { 
            await userManager.AddToRoleAsync(admin, IdentityRoleConstants.Admin.ToString());
        }
    }
}
