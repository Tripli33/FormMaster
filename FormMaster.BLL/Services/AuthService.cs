using AutoMapper;
using FormMaster.BLL.DTOs;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace FormMaster.BLL.Services;

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

    public Task<IdentityResult> RegisterAsync(UserRegistrationDto registrationDto)
    {
        var user = mapper.Map<User>(registrationDto);
        var result = userManager.CreateAsync(user, registrationDto.Password!);

        return result;
    }
}
