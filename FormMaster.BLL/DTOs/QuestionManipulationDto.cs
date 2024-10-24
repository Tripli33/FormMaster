using FormMaster.DAL.Entities;

namespace FormMaster.BLL.DTOs;

public class QuestionManipulationDto
{
    public int QuestionId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public QuestionType QuestionType { get; set; }
}
