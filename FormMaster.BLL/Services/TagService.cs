using FormMaster.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.BLL.Services;

public class TagService(ITagRepository repository) : ITagService
{
    // TODO REMOVE SELECT OP
    public async Task<ICollection<string>> GetAllAsync()
    {
        var tags = await repository.GetAll().Select(t => t.Name).ToListAsync();

        return tags;
    }
}
