using AutoMapper;
using FormMaster.BLL.DTOs;
using FormMaster.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.BLL.Services.Implementations;

public class QuestionService(IQuestionRepository questionRepository, IMapper mapper) : IQuestionService
{
    public async Task<ICollection<QuestionRegistrationDto>> GetByTemplateIdAsync(int templateId)
    {
        var questions = await questionRepository.GetByCondition(q => q.TemplateId == templateId).ToListAsync();
        var questionDtos = mapper.Map<ICollection<QuestionRegistrationDto>>(questions);

        return questionDtos;
    }

    public async Task<ICollection<QuestionManipulationDto>> GetPublicByTemplateIdAsync(int templateId)
    {
        var questions = await questionRepository.GetByCondition(q => q.TemplateId == templateId && q.IsVisible).ToListAsync();
        var questionDtos = mapper.Map<ICollection<QuestionManipulationDto>>(questions);

        return questionDtos;
    }
}
