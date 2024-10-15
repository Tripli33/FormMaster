using System.ComponentModel.DataAnnotations;

namespace FormMaster.BLL.DTOs;

public class UserLoginDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [MinLength(4)]
    public string? Password { get; set; }
}
