using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaster.WEB.Pages;

[Authorize]
public class FormResultModel(IFormService formService) : PageModel
{
    public Form? Form { get; set; }
    public async Task OnGetAsync([FromQuery]int formId)
    {
        TempData["FormId"] = formId;
        Form = await formService.GetAsync(formId);
    }

    public ActionResult OnPostUpdate()
    {
        var formId = TempData["FormId"];

        return RedirectToPage("FormUpdate", new { FormId = formId });
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var formId = TempData["FormId"];
        await formService.DeleteAsync(Convert.ToInt32(formId));
        return RedirectToPage("Index");
    }
}
