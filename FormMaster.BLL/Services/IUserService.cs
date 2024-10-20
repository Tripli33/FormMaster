namespace FormMaster.BLL.Services;

public interface IUserService
{
    Task<ICollection<string?>> GetAllAsync();
}
