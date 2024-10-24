using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaster.WEB.Pages;

[Authorize]
public class TemplateModel(ITemplateService templateService, IFormService formService) : PageModel
{
    public TemplateManipulationDto? Template { get; set; }
    public ICollection<TemplateFormDto> FormDtos { get; set; }
    public async Task OnGetAsync([FromQuery]int templateId)
    {
        TempData["TemplateId"] = templateId;
        Template = await templateService.GetAsync(templateId);
        FormDtos = await formService.GetByTemplateIdAsync(templateId);
    }

    public ActionResult OnPostSolve()
    {
        var templateId = TempData["TemplateId"];

        return RedirectToPage("Form", new { TemplateId = templateId });
    }

    public IActionResult OnPostUpdate()
    {
        var templateId = TempData["TemplateId"];

        return RedirectToPage("QuestionsConstructor", new { TemplateId = templateId });
    }
}
