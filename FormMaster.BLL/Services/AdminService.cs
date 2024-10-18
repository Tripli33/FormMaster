using FormMaster.BLL.DTOs;
using FormMaster.DAL.DataContext;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.BLL.Services;

// TODO: DON'T USE CONTEXT IN BLL LAYER
public class AdminService(FormMasterDbContext context, UserManager<User> userManager, 
    RoleManager<Role> roleManager) : IAdminService
{
    public async Task BlockUsersByIdAsync(IEnumerable<int> ids)
    {
        var users = await GetUsersByIds(ids);

        foreach (var user in users)
        {
            user.LockoutEnd = DateTimeOffset.MaxValue;
        }

        context.Users.UpdateRange(users);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUsersByIdAsync(IEnumerable<int> ids)
    {
        var users = await GetUsersByIds(ids);

        context.Users.RemoveRange(users);
        await context.SaveChangesAsync();
    }

    public async Task DemoteUsersByIdAsync(IEnumerable<int> ids)
    {
        var adminRole = await roleManager.FindByNameAsync(IdentityRoleConstants.Admin.ToString());
        var adminUserRoles = await context.UserRoles
            .Where(ur => ids.Contains(ur.UserId) && ur.RoleId == adminRole!.Id).ToListAsync();

        context.UserRoles.RemoveRange(adminUserRoles);
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<AdminUserManipulationDto>> GetAllUsersAsync()
    {
        var userRoles = context.UserRoles.Include(ur => ur.User);
        var adminRole = await roleManager.FindByNameAsync(IdentityRoleConstants.Admin.ToString());
        var userDtos = await userRoles.GroupBy(u => u.User).Select(g => new AdminUserManipulationDto
        {
            Id = g.Key!.Id,
            UserName = g.Key.UserName,
            Email = g.Key.Email,
            IsBlocked = g.Key.LockoutEnd != null,
            IsAdmin = g.Any(ur => ur.RoleId == adminRole!.Id)
        }).ToListAsync(); 

        return userDtos;
    }

    public async Task PromoteUsersByIdAsync(IEnumerable<int> ids)
    {
        var users = await GetUsersByIds(ids);
        var adminRole = await roleManager.FindByNameAsync(IdentityRoleConstants.Admin.ToString());
        var userRoles = users.Select(u => new UserRole { UserId = u.Id, RoleId = adminRole!.Id });

        context.AddRange(userRoles);
        await context.SaveChangesAsync();
    }

    public async Task UnblockUsersByIdAsync(IEnumerable<int> ids)
    {
        var users = await GetUsersByIds(ids);

        foreach (var user in users)
        {
            user.LockoutEnd = null;
        }

        context.Users.UpdateRange(users);
        await context.SaveChangesAsync();
    }

    private async Task<IEnumerable<User>> GetUsersByIds(IEnumerable<int> ids)
    {
        var users = await userManager.Users.Where(u => ids.Contains(u.Id)).ToListAsync();

        return users;
    }
}
