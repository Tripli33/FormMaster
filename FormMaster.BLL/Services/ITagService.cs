namespace FormMaster.BLL.Services;

public interface ITagService
{
    Task<ICollection<string>> GetAllAsync();
}
