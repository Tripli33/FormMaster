namespace FormMaster.BLL.DTOs;

public class AdminUserManipulationDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsAdmin { get; set; }
}
