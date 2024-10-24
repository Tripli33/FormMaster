using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaster.WEB.Pages;

public class SearchResultModel(ITemplateService templateService) : PageModel
{
    [BindProperty]
    public ICollection<HomeTemplateManipulationDto>? Templates { get; set; } = [];

    public async Task OnGet([FromQuery] string tag)
    {
        if (!string.IsNullOrEmpty(tag))
        {
            Templates = await templateService.GetByTagNameAsync(tag);
        }
    }

    public async Task<IActionResult> OnPostSearchByTerm([FromForm] string search)
    {
        if (!string.IsNullOrEmpty(search))
        {
            Templates = await templateService.GetByTermAsync(search);
        }

        return Page();
    }
}
