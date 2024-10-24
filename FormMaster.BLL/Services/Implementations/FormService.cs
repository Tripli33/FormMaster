using AutoMapper;
using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;
using FormMaster.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.BLL.Services.Implementations;

public class FormService(IFormRepository formRepository, IMapper mapper) : IFormService
{
    public async Task<Form> CreateAsync(int templateId, int userId, ICollection<FormAnswerDto> answers)
    {
        var form = new Form
        {
            UserId = userId,
            TemplateId = templateId,
            Answers = answers.Select(a => new Answer { QuestionId = a.QuestionId,  Name = a.Name }).ToList()
        };

        await formRepository.Add(form);

        return form;
    }

    public async Task DeleteAsync(int formId)
    {
        var form = formRepository.GetById(formId);
        if (form != null)
        {
            await formRepository.Delete(form);
        }
    }

    public async Task<ICollection<UserFormManipulationDto>> GetByUserIdAsync(int userId)
    {
        var forms = await formRepository.GetByCondition(t => t.UserId == userId).Include(t => t.Template).ToListAsync();
        var formDtos = mapper.Map<ICollection<UserFormManipulationDto>>(forms);

        return formDtos;
    }

    public async Task<Form> GetAsync(int formId)
    {
        var form = await formRepository.GetByCondition(f => f.FormId == formId).Include(f => f.Answers)
            .ThenInclude(a => a.Question).FirstOrDefaultAsync();

        return form;
    }

    public async Task UpdateAsync(int formId, ICollection<FormAnswerDto> answerDtos)
    {
        var form = await formRepository.GetByCondition(f => f.FormId == formId).Include(f => f.Answers).FirstOrDefaultAsync();

        if (form != null)
        {
            var answers = mapper.Map<ICollection<Answer>>(answerDtos);
            form.Answers = answers;
            await formRepository.Update(form);
        }
    }

    public async Task<ICollection<TemplateFormDto>> GetByTemplateIdAsync(int templateId)
    {
        var forms = await formRepository.GetByCondition(f => f.TemplateId == templateId).Include(f => f.User).ToListAsync();
        var formDtos = mapper.Map<ICollection<TemplateFormDto>>(forms);

        return formDtos;
    }
}
