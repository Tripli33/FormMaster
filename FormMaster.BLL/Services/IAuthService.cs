using FormMaster.BLL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FormMaster.BLL.Services;

public interface IAuthService
{
    Task<SignInResult> LoginAsync(UserLoginDto loginDto);
    Task LogoutAsync();
    Task<IdentityResult> RegisterAsync(UserRegistrationDto registrationDto);
    Task UpdateUserClaimsById(int id);
}