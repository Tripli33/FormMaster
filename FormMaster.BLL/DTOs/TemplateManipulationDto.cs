using FormMaster.DAL.Entities;

namespace FormMaster.BLL.DTOs;

public class TemplateManipulationDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Topic? Topic { get; set; }
    public User? User { get; set; }
    public ICollection<Question>? Questions { get; set; }
    public ICollection<Tag>? Tags { get; set; }
}
