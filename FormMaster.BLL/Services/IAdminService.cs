using FormMaster.BLL.DTOs;

namespace FormMaster.BLL.Services;

public interface IAdminService
{
    Task<ICollection<AdminUserManipulationDto>> GetAllUsersAsync();
    Task DeleteUsersByIdAsync(IEnumerable<int> ids);
    Task BlockUsersByIdAsync(IEnumerable<int> ids);
    Task UnblockUsersByIdAsync(IEnumerable<int> ids);
    Task DemoteUsersByIdAsync(IEnumerable<int> ids);
    Task PromoteUsersByIdAsync(IEnumerable<int> ids);
}
