using System.ComponentModel.DataAnnotations;

namespace FormMaster.BLL.DTOs;

public class TemplateRegistrationDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public int TopicId { get; set; }
    [Required]
    public string? Tags { get; set; }
    public string? AllowedUsers { get; set; }
}

