namespace FormMaster.DAL.Entities;

public class Topic
{
    public int TopicId { get; set; }
    public string? Name { get; set; }
    public ICollection<Template>? Templates { get; set; }
}
