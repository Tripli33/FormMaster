using AutoMapper;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.BLL.Services;

public class UserService(UserManager<User> userManager, IMapper mapper) : IUserService
{
    // TODO REMOVE SELECT OP
    public async Task<ICollection<string?>> GetAllAsync()
    {
        var users = await userManager.Users.Select(u => u.Email).ToListAsync();

        return users;
    }
}
