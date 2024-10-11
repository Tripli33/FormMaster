using Microsoft.AspNetCore.Identity;

namespace FormMaster.DAL.Entities;

public class User : IdentityUser<int>
{
    public ICollection<Template>? Templates { get; set; }
    public ICollection<Form>? Forms { get; set; }
    public ICollection<Template>? AllowedTemplates { get; set; }
}
