using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;
using FormMaster.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.BLL.Services;

public class TagService(ITagRepository repository) : ITagService
{
    public async Task CreateIfNotExistsAsync(ICollection<string> names)
    {
        var existingTags = await repository.GetAll().Where(t => names.Contains(t.Name!))
            .Select(t => t.Name).ToListAsync();
        var newTags = names.Where(t => !existingTags.Contains(t)).ToList();
        var tagsToAdd = newTags.Select(tagName => new Tag { Name = tagName }).ToList();

        await repository.AddRange(tagsToAdd);
    }

    public async Task<ICollection<string>> GetAllAsync()
    {
        var tags = await repository.GetAll().Select(t => t.Name).ToListAsync();

        return tags;
    }

    public async Task<ICollection<Tag>> GetByNamesAsync(ICollection<string> names)
    {
        var tags = await repository.GetAll().Where(t => names.Contains(t.Name!)).ToListAsync();

        return tags;
    }
}
