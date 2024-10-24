using FormMaster.DAL.Entities;

namespace FormMaster.BLL.Services.Contracts;

public interface ITemplateSearchService
{
    Task<IEnumerable<Template>> SearchTemplatesAsync(string searchTerm);
    Task<IEnumerable<Template>> SearchTemplatesByTagAsync(string tagName);
    Task AddIndexAsync(Template template);
    Task DeleteIndexAsync();
}
