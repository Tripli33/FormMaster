using AutoMapper;
using Elastic.Clients.Elasticsearch.IndexManagement;
using FormMaster.BLL.DTOs;
using FormMaster.BLL.Services.Contracts;
using FormMaster.DAL.Entities;
using FormMaster.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FormMaster.BLL.Services.Implementations;

public class TemplateService(ITemplateRepository templateRepository,
    ITagService tagService, IUserService userService, IMapper mapper, UserManager<User> userManager,
    ITemplateSearchService templateSearchService) : ITemplateService
{
    public async Task AddQuestionsAsync(int templateId, ICollection<QuestionRegistrationDto> questionsDto)
    {
        var template = await templateRepository.GetByCondition(t => t.TemplateId == templateId)
            .Include(t => t.Questions).FirstOrDefaultAsync();
        var questions = mapper.Map<ICollection<Question>>(questionsDto);
        if (template != null)
        {
            // TODO Fix problem with question constructor 
            template.Questions!.Clear();
            template.Questions = questions;
            await templateRepository.Update(template);
        }
    }

    public async Task<DAL.Entities.Template> CreateAsync(int userId, TemplateRegistrationDto registrationDto)
    {
        var tagsData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(registrationDto.Tags!);
        var tags = tagsData?.ConvertAll(tag => tag["value"]).ToArray() ?? [];
        await tagService.CreateIfNotExistsAsync(tags);
        var tagsToAdd = await tagService.GetByNamesAsync(tags);
        var template = new DAL.Entities.Template()
        {
            Name = registrationDto.Name,
            Description = registrationDto.Description,
            IsPublic = registrationDto.IsPublic,
            CreationDate = DateTime.UtcNow,
            TopicId = registrationDto.TopicId,
            UserId = userId,
            Tags = tagsToAdd
        };

        if (!registrationDto.IsPublic)
        {
            var usersData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(registrationDto.AllowedUsers ?? string.Empty);
            var users = usersData?.ConvertAll(tag => tag["value"]).ToArray() ?? [];
            var usersToAdd = await userService.GetByEmailsAsync(users);
            template.AllowedUsers = usersToAdd;
        }

        await templateRepository.Add(template);
        await templateSearchService.AddIndexAsync(template);

        return template;
    }

    public async Task<ICollection<UserTemplateManipulationDto>> GetAllAsync(int userId)
    {
        var templates = await templateRepository.GetByCondition(t => t.UserId == userId).ToListAsync();
        var templateDtos = mapper.Map<ICollection<UserTemplateManipulationDto>>(templates);

        return templateDtos;
    }

    public async Task<TemplateManipulationDto> GetAsync(int templateId)
    {
        var template = await templateRepository.GetByCondition(t => t.TemplateId == templateId)
            .Include(t => t.Questions).Include(t => t.Tags).Include(t => t.User).Include(t => t.Topic)
            .FirstOrDefaultAsync();
        var templateDto = mapper.Map<TemplateManipulationDto>(template);

        return templateDto;
    }

    public async Task<ICollection<HomeTemplateManipulationDto>> GetByTagNameAsync(string tagName)
    {
        var templates = await templateSearchService.SearchTemplatesByTagAsync(tagName);
        var templateDtos = mapper.Map<ICollection<HomeTemplateManipulationDto>>(templates);

        return templateDtos;
    }

    public async Task<ICollection<HomeTemplateManipulationDto>> GetByTermAsync(string searchTerm)
    {
        var templates = await templateSearchService.SearchTemplatesAsync(searchTerm);
        var templateDtos = mapper.Map<ICollection<HomeTemplateManipulationDto>>(templates);

        return templateDtos;
    }

    public async Task<ICollection<HomeTemplateManipulationDto>> GetLatestForAuthorizedUserAsync(int userId)
    {
        var templates = await templateRepository
            .GetByCondition(t => t.IsPublic || t.AllowedUsers.Any(u => u.Id == userId))
            .Include(t => t.User)
            .Include(t => t.AllowedUsers)
            .OrderByDescending(t => t.CreationDate)
            .ToListAsync();
        var templateDtos = mapper.Map<ICollection<HomeTemplateManipulationDto>>(templates);

        return templateDtos;
    }

    public async Task<ICollection<HomeTemplateManipulationDto>> GetLatestForNonAuthorizedUserAsync()
    {
        var templates = await templateRepository
            .GetByCondition(t => t.IsPublic)
            .Include(t => t.User)
            .OrderByDescending(t => t.CreationDate)
            .ToListAsync();
        var templateDtos = mapper.Map<ICollection<HomeTemplateManipulationDto>>(templates);

        return templateDtos;
    }

    public async Task<ICollection<HomeTemplateManipulationDto>> GetTopForAuthorizedUserAsync(int userId, int top)
    {
        var templates = await templateRepository
            .GetByCondition(t => t.IsPublic || t.AllowedUsers.Any(u => u.Id == userId))
            .Include(t => t.User)
            .Include(t => t.AllowedUsers)
            .OrderByDescending(t => t.Count)
            .Take(top)
            .ToListAsync();
        var templateDtos = mapper.Map<ICollection<HomeTemplateManipulationDto>>(templates);

        return templateDtos;
    }

    public async Task<ICollection<HomeTemplateManipulationDto>> GetTopForNonAuthorizedUserAsync(int top)
    {
        var templates = await templateRepository
            .GetByCondition(t => t.IsPublic)
            .Include(t => t.User)
            .OrderByDescending(t => t.Count)
            .Take(top)
            .ToListAsync();
        var templateDtos = mapper.Map<ICollection<HomeTemplateManipulationDto>>(templates);

        return templateDtos;
    }
}
