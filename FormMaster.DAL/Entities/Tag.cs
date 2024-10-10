namespace FormMaster.DAL.Entities;

public class Tag
{
    public int TagId { get; set; }
    public string? Name { get; set; }
    public ICollection<Template>? Templates { get; set; }
}
