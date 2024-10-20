using System.ComponentModel.DataAnnotations;

namespace FormMaster.BLL.DTOs;

public class TemplateRegistrationDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    [Required]
    public int TopicId { get; set; }
    [Required]
    public string? Tags { get; set; }
    public string? AllowedUsers { get; set; }
}

