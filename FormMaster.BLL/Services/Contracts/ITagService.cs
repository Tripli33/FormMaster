using FormMaster.DAL.Entities;

namespace FormMaster.BLL.Services.Contracts;

public interface ITagService
{
    Task<ICollection<string>> GetAllAsync();
    Task<ICollection<Tag>> GetByNamesAsync(ICollection<string> names);
    Task CreateIfNotExistsAsync(ICollection<string> names);
}
