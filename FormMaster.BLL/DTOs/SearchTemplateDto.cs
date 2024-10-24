using FormMaster.DAL.Entities;

namespace FormMaster.BLL.DTOs;

public class SearchTemplateDto
{
    public int TemplateId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
    public int UserId { get; set; }
    public SearchUserDto? User { get; set; }
    public ICollection<SearchTagDto>? Tags { get; set; }

}
