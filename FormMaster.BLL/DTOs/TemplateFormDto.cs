using FormMaster.DAL.Entities;

namespace FormMaster.BLL.DTOs;

public class TemplateFormDto
{
    public int FormId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
