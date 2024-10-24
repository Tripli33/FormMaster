using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services;
using FormMaster.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text.Json;

namespace FormMaster.WEB.Pages;

[Authorize]
public class TemplateConstructorModel(ITagService tagService, IUserService userService, 
    ITopicService topicService, ITemplateService templateService) : PageModel
{
    [BindProperty]
    public TemplateRegistrationDto? RegistrationDto { get; set; }
    public SelectList? Topics { get; set; }

    public async Task OnGetAsync()
    {
        var topics = await topicService.GetAllAsync();
        Topics = new SelectList(topics, "TopicId", "Name");
        var tags = await tagService.GetAllAsync();
        var users = await userService.GetAllAsync();
        ViewData["Tags"] = JsonSerializer.Serialize(tags);
        ViewData["Users"] = JsonSerializer.Serialize(users);
        TempData["Topics"] = JsonSerializer.Serialize(topics);
        TempData["Tags"] = ViewData["Tags"];
        TempData["Users"] = ViewData["Users"];
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["Tags"] = TempData["Tags"];
            ViewData["Users"] = TempData["Users"];
            var topicsData = TempData["Topics"] as string;
            var topics = JsonSerializer.Deserialize<List<TopicTemplateManipulationDto>>(topicsData ?? string.Empty);
            Topics = new SelectList(topics, "TopicId", "Name");
            TempData.Keep();

            return Page();
        }

        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var template = await templateService.CreateAsync(Convert.ToInt32(userId), RegistrationDto!);
        TempData.Clear();

        return RedirectToPage("QuestionsConstructor", new { template.TemplateId });
    }
}
