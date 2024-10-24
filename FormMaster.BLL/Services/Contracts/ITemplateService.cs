using FormMaster.BLL.DTOs;
using FormMaster.DAL.Entities;

namespace FormMaster.BLL.Services.Contracts;

public interface ITemplateService
{
    Task<Template> CreateAsync(int userId, TemplateRegistrationDto registrationDto);
    Task AddQuestionsAsync(int templateId, ICollection<QuestionRegistrationDto> questionsDto);
    Task<TemplateManipulationDto> GetAsync(int templateId);
    Task<ICollection<UserTemplateManipulationDto>> GetAllAsync(int userId);
    Task<ICollection<HomeTemplateManipulationDto>> GetTopForNonAuthorizedUserAsync(int top);
    Task<ICollection<HomeTemplateManipulationDto>> GetTopForAuthorizedUserAsync(int userId, int top);
    Task<ICollection<HomeTemplateManipulationDto>> GetLatestForNonAuthorizedUserAsync();
    Task<ICollection<HomeTemplateManipulationDto>> GetLatestForAuthorizedUserAsync(int userId);
    Task<ICollection<HomeTemplateManipulationDto>> GetByTagNameAsync(string tagName);
    Task<ICollection<HomeTemplateManipulationDto>> GetByTermAsync(string searchTerm);
}
