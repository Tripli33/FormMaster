using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaster.WEB.Pages;

[Authorize]
public class UserModel(IFormService formService, ITemplateService templateService) : PageModel
{
    public ICollection<UserTemplateManipulationDto>? TemplateDtos { get; set; }
    public ICollection<UserFormManipulationDto>? FormDtos { get; set; }
    public async Task OnGetAsync([FromQuery]int userId)
    {
        TemplateDtos = await templateService.GetAllAsync(userId);
        FormDtos = await formService.GetByUserIdAsync(userId);
    }
}
