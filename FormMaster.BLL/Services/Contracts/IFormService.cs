using FormMaster.BLL.DTOs;
using FormMaster.DAL.Entities;

namespace FormMaster.BLL.Services.Contracts;

public interface IFormService
{
    Task<Form> CreateAsync(int templateId, int userId, ICollection<FormAnswerDto> answers);
    Task<Form> GetAsync(int formId);
    Task<ICollection<UserFormManipulationDto>> GetByUserIdAsync(int userId);
    Task<ICollection<TemplateFormDto>> GetByTemplateIdAsync(int templateId);
    Task UpdateAsync(int formId, ICollection<FormAnswerDto> answerDtos);
    Task DeleteAsync(int formId);
}
