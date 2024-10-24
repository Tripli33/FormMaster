using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaster.WEB.Pages;

[Authorize]
public class FormUpdateModel(IFormService formService) : PageModel
{
    [BindProperty]
    public List<FormAnswerDto>? Answers { get; set; }
    public Form? Form { get; set; }
    public async Task OnGetAsync([FromQuery] int formId)
    {
        TempData["FormId"] = formId;
        Form = await formService.GetAsync(formId);
    }

    public async Task<IActionResult> OnPostUpdateAsync()
    {
        var formId = TempData["FormId"];
        await formService.UpdateAsync(Convert.ToInt32(formId), Answers);

        return RedirectToPage("FormResult", new { FormId = formId });
    }
}
