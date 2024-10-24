using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace FormMaster.WEB.Pages;

[Authorize(Roles = "Admin")]
public class AdminPanelModel(IAdminService adminService, IAuthService authService) : PageModel
{
    [BindProperty]
    public List<int> AreChecked { get; set; } = [];
    public ICollection<AdminUserManipulationDto>? ManipulationDtos { get; set; }

    public async Task OnGetAsync()
    {
        ManipulationDtos = await adminService.GetAllUsersAsync();
        SerializeAdminTableTempData();
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var temp = DeserializeAdminTableTempData();
        ManipulationDtos = temp?.Where(md => !AreChecked.Contains(md.Id)).ToList();
        
        SerializeAdminTableTempData();
        await adminService.DeleteUsersByIdAsync(AreChecked);

        if (IsUserPicked())
        {
            await authService.LogoutAsync();

            return RedirectToPage("Login");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostBlockAsync()
    {
        UpdatePickedAdminTableData(users =>
        {
            foreach (var user in users)
            {
                user.IsBlocked = true;
            }
        }); 
        await adminService.BlockUsersByIdAsync(AreChecked);

        if (IsUserPicked())
        {
            await authService.LogoutAsync();

            return RedirectToPage("Login");
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostUnblockAsync()
    {
        UpdatePickedAdminTableData(users =>
        {
            foreach (var user in users)
            {
                user.IsBlocked = false;
            }
        });
        await adminService.UnblockUsersByIdAsync(AreChecked);

        if (IsUserPicked())
        {
            await authService.LogoutAsync();

            return RedirectToPage("Login");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDemoteAsync()
    {
        UpdatePickedAdminTableData(users =>
        {
            foreach (var user in users)
            {
                user.IsAdmin = false;
            }
        });
        await adminService.DemoteUsersByIdAsync(AreChecked);

        if (IsUserPicked())
        {
            var id = GetUserIdFromClaims();
            await authService.UpdateUserClaimsById(id);

            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostPromoteAsync()
    {
        UpdatePickedAdminTableData(users =>
        {
            foreach (var user in users)
            {
                user.IsAdmin = true;
            }
        });
        await adminService.PromoteUsersByIdAsync(AreChecked);

        return Page();
    }

    private void SerializeAdminTableTempData()
    {
        TempData["AdminTableData"] = JsonSerializer.Serialize(ManipulationDtos);
    }

    private List<AdminUserManipulationDto>? DeserializeAdminTableTempData()
    {
        var serializedData = TempData["AdminTableData"] as string;
        var temp = JsonSerializer.Deserialize<List<AdminUserManipulationDto>>(serializedData ?? string.Empty);

        return temp;
    }

    private void UpdatePickedAdminTableData(Action<List<AdminUserManipulationDto>> updateTableAction)
    {
        ManipulationDtos = DeserializeAdminTableTempData();
        var users = ManipulationDtos?.Where(md => AreChecked.Contains(md.Id)).ToList();

        if (users is not null)
        {
            updateTableAction(users);
        }

        SerializeAdminTableTempData();
    }

    private bool IsUserPicked()
    {
        return AreChecked.Contains(GetUserIdFromClaims());      
    }

    private int GetUserIdFromClaims()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Convert.ToInt32(userId);
    }
}