using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace FormMaster.Pages;


public class IndexModel(ITagService tagService, ITemplateService templateService) : PageModel
{
    public ICollection<HomeTemplateManipulationDto>? TopTemplates { get; set; } = [];
    public ICollection<HomeTemplateManipulationDto>? LatestTemplates { get; set; } = [];
    public async Task OnGetAsync()
    {
        var tags = await tagService.GetAllAsync();
        ViewData["Tags"] = JsonSerializer.Serialize(tags);
        if (User.Identity.IsAuthenticated)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            TopTemplates = await templateService.GetTopForAuthorizedUserAsync(Convert.ToInt32(userId), 5);
            LatestTemplates = await templateService.GetLatestForAuthorizedUserAsync(Convert.ToInt32(userId));
        }
        else
        {
            TopTemplates = await templateService.GetTopForNonAuthorizedUserAsync(5);
            LatestTemplates = await templateService.GetLatestForNonAuthorizedUserAsync();
        }
    }

    public IActionResult OnGetLoadMoreTemplatesAsync([FromQuery]int page = 1)
    {
        var templates = LatestTemplates?.Skip((page - 1) * 5).Take(5);

        return new JsonResult(templates);
    }
}
