namespace FormMaster.DAL.Entities;

public class User
{
    public int UserId { get; set; }
    public ICollection<Template>? Templates { get; set; }
    public ICollection<Form>? Forms { get; set; }
    public ICollection<Template>? AllowedTemplates { get; set; }
}
