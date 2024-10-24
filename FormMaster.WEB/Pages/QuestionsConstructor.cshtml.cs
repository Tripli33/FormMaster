using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services;
using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace FormMaster.WEB.Pages;

[Authorize]
public class QuestionsConstructorModel(ITemplateService templateService, IQuestionService questionService) : PageModel
{
    [BindProperty]
    public QuestionRegistrationDto? QuestionRegistrationDto { get; set; }
    public List<QuestionRegistrationDto>? Questions { get; set; }
    public SelectList QuestionTypes { get; set; } = new SelectList(Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>());

    public async Task OnGetAsync([FromQuery]int templateId)
    {
        var questions = await questionService.GetByTemplateIdAsync(templateId);
        Questions = questions.ToList();
        TempData["TemplateId"] = templateId;
        TempData["Questions"] = JsonSerializer.Serialize(Questions);
    }

    public IActionResult OnPostAddQuestion()
    {
        var tempData = TempData["Questions"] as string;
        Questions = JsonSerializer.Deserialize<List<QuestionRegistrationDto>>(tempData ?? string.Empty);
        if (ModelState.IsValid)
        {
            Questions!.Add(QuestionRegistrationDto!);
            TempData["Questions"] = JsonSerializer.Serialize(Questions);
            ModelState.Clear();
            QuestionRegistrationDto = new();
        }

        
        TempData.Keep();
        
        return Page();
    }

    public IActionResult OnPostDeleteQuestion(int index)
    {
        var tempData = TempData["Questions"] as string;
        Questions = JsonSerializer.Deserialize<List<QuestionRegistrationDto>>(tempData ?? string.Empty);
        Questions!.RemoveAt(index);
        TempData["Questions"] = JsonSerializer.Serialize(Questions);
        TempData.Keep();

        return Page();
    }

    public async Task<IActionResult> OnPostSubmitAsync()
    {
        var tempData = TempData["Questions"] as string;
        if (tempData == "[]")
        {
            ModelState.AddModelError(string.Empty, "Template must have a questions");
            TempData.Keep();

            return Page();
        }

        Questions = JsonSerializer.Deserialize<List<QuestionRegistrationDto>>(tempData ?? string.Empty);
        var templateId = TempData["TemplateId"];
        TempData.Clear();
        await templateService.AddQuestionsAsync(Convert.ToInt32(templateId), Questions!);

        return RedirectToPage("Template", new { TemplateId = templateId });
    }
}
