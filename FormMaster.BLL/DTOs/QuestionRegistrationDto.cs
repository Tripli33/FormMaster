using FormMaster.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace FormMaster.BLL.DTOs;

public class QuestionRegistrationDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    public QuestionType QuestionType { get; set; }
    public bool IsVisible { get; set; }
}
