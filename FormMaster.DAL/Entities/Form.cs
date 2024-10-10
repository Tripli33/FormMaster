namespace FormMaster.DAL.Entities;

public class Form
{
    public int FormId { get; set; }
    public int TemplateId { get; set; }
    public Template? Template { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Answer>? Answers { get; set; }
}
