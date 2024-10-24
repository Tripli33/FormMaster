using FormMaster.BLL.DTOs;

namespace FormMaster.BLL.Services.Contracts;

public interface ITemplateSearchService
{
    Task<IEnumerable<SearchTemplateDto>> SearchTemplatesAsync(string searchTerm);
    Task<IEnumerable<SearchTemplateDto>> SearchTemplatesByTagAsync(string tagName);
    Task AddIndexAsync(SearchTemplateDto template);
    Task DeleteIndexAsync();
}
