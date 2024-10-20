using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;


namespace FormMaster.WEB.Pages
{
    public class TemplateConstructorModel(ITagService tagService, IUserService userService, ITopicService topicService) : PageModel
    {
        [BindProperty]
        public TemplateRegistrationDto RegistrationDto { get; set; }
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

        public IActionResult OnPostAsync()
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

            return RedirectToPage();
        }
    }
}
