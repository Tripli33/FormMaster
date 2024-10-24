using AutoMapper;
using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.BLL.Services.Implementations;

public class UserService(UserManager<User> userManager, IMapper mapper) : IUserService
{
    public async Task<ICollection<string?>> GetAllAsync()
    {
        var users = await userManager.Users.Select(u => u.Email).ToListAsync();

        return users;
    }

    public async Task<ICollection<User>> GetByEmailsAsync(ICollection<string> emails)
    {
        var users = await userManager.Users.Where(u => emails.Contains(u.Email!)).ToListAsync();

        return users;
    }
}
