using FormMaster.BLL.DTOs;

namespace FormMaster.BLL.Services;

public interface IQuestionService
{
    Task<ICollection<QuestionRegistrationDto>> GetByTemplateIdAsync(int templateId);
    Task<ICollection<QuestionManipulationDto>> GetPublicByTemplateIdAsync(int templateId);
}
