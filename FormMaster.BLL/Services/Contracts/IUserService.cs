using FormMaster.DAL.Entities;

namespace FormMaster.BLL.Services.Contracts;

public interface IUserService
{
    Task<ICollection<string?>> GetAllAsync();
    Task<ICollection<User>> GetByEmailsAsync(ICollection<string> emails);
}
