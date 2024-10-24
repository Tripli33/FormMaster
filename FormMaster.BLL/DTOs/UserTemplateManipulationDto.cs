namespace FormMaster.BLL.DTOs;

public class UserTemplateManipulationDto
{
    public int TemplateId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreationDate { get; set; }
}
