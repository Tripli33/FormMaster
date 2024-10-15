using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaster.WEB.Pages;

public class RegisterModel(IAuthService authService) : PageModel
{
    [BindProperty]
    public UserRegistrationDto? RegistrationDto { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var result = await authService.RegisterAsync(RegistrationDto);

            if (result.Succeeded)
            {
                return RedirectToPage("Login");
            }

            ModelState.AddModelError(string.Empty, result.Errors.First().Description);
        }

        return Page();
    }


}
