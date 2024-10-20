using FormMaster.BLL.DTOs;

namespace FormMaster.BLL.Services;

public interface ITopicService
{
    Task<ICollection<TopicTemplateManipulationDto>> GetAllAsync();
}
