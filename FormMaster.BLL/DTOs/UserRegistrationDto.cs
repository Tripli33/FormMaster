using System.ComponentModel.DataAnnotations;

namespace FormMaster.BLL.DTOs;

public class UserRegistrationDto
{
    [Required]
    [StringLength(30, ErrorMessage = "Username must be 3-30 characters", MinimumLength = 3)]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [MinLength(4)]
    public string? Password { get; set; }
}
