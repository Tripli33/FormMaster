using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaster.WEB.Pages;

public class LoginModel(IAuthService authService) : PageModel
{
    [BindProperty]
    public UserLoginDto? LoginDto { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var result = await authService.LoginAsync(LoginDto);

            if (result.Succeeded)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Incorrect login or password");
        }

        return Page();
    }
}
