using Microsoft.AspNetCore.Identity;

namespace FormMaster.DAL.Entities;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole>? UserRoles { get; set; }

    public Role()
    {
        
    }

    public Role(string roleName) : base(roleName)
    {
        
    }
}
