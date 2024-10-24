using FormMaster.DAL.Entities;

namespace FormMaster.BLL.DTOs;

public class HomeTemplateManipulationDto
{
    public int TemplateId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
