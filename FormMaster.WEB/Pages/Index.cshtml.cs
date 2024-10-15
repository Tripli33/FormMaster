using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaster.Pages;

[Authorize]
public class IndexModel : PageModel
{
}
