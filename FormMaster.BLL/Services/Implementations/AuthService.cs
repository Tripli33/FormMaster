using AutoMapper;
using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace FormMaster.BLL.Services.Implementations;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
    IMapper mapper) : IAuthService
{
    public async Task<SignInResult> LoginAsync(UserLoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email!);
        if (user is null)
        {
            return SignInResult.Failed;
        }

        var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

        return result;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> RegisterAsync(UserRegistrationDto registrationDto)
    {
        var user = mapper.Map<User>(registrationDto);
        var result = await userManager.CreateAsync(user, registrationDto.Password!);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, IdentityRoleConstants.User.ToString());
        }

        return result;
    }

    public async Task UpdateUserClaimsById(int id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if (user is not null)
        {
            await signInManager.RefreshSignInAsync(user);
        }
    }
}
