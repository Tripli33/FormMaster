using Microsoft.AspNetCore.Identity;

namespace FormMaster.DAL.Entities;

public class UserRole : IdentityRole<int>
{
    public UserRole()
    {
        
    }

    public UserRole(string roleName) : base(roleName)
    {
        
    }
}
