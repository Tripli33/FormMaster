using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services;
using FormMaster.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace FormMaster.WEB.Pages;

[Authorize]
public class FormModel(IQuestionService questionService, IFormService formService) : PageModel
{
    public ICollection<QuestionManipulationDto>? Questions { get; set; }
    [BindProperty]
    public List<FormAnswerDto>? Answers { get; set; }
    public async Task OnGetAsync([FromQuery]int templateId)
    {
        TempData["TemplateId"] = templateId;
        Questions = await questionService.GetPublicByTemplateIdAsync(templateId);
        TempData["Questions"] = JsonSerializer.Serialize(Questions);
    }

    public async Task<IActionResult> OnPostSubmitAsync()
    {
        if (!ModelState.IsValid)
        {
            var tempData = TempData["Questions"] as string;
            Questions = JsonSerializer.Deserialize<List<QuestionManipulationDto>>(tempData ?? string.Empty);
            TempData.Keep();

            return Page();
        }

        var templateId = TempData["TemplateId"];
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var form = await formService.CreateAsync(Convert.ToInt32(templateId), Convert.ToInt32(userId), Answers);
        TempData.Clear();
        
        return RedirectToPage("FormResult", new { form.FormId});
    }
}
