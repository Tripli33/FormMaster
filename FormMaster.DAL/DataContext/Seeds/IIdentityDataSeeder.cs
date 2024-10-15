namespace FormMaster.DAL.DataContext.Seeds;

public interface IIdentityDataSeeder
{
    Task SeedRolesAsync();
    Task SeedAdminUserAsync();
}
