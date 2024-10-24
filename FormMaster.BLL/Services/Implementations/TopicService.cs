using AutoMapper;
using FormMaster.BLL.DTOs;
using FormMaster.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.BLL.Services.Implementations;

public class TopicService(ITopicRepository topicRepository, IMapper mapper) : ITopicService
{
    public async Task<ICollection<TopicTemplateManipulationDto>> GetAllAsync()
    {
        var topics = await topicRepository.GetAll().ToListAsync();
        var topicDtos = mapper.Map<ICollection<TopicTemplateManipulationDto>>(topics);

        return topicDtos;
    }
}
