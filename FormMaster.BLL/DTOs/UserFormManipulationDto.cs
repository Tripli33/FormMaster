using FormMaster.DAL.Entities;

namespace FormMaster.BLL.DTOs;

public class UserFormManipulationDto
{
    public int FormId { get; set; }
    public int TemplateId { get; set; }
    public Template? Template { get; set; }
}
