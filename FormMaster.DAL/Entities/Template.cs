namespace FormMaster.DAL.Entities;

public class Template
{
    public int TemplateId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreationDate { get; set; }
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int Count { get; set; }
    public ICollection<Question>? Questions { get; set; }
    public ICollection<Form>? Forms { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<User>? AllowedUsers { get; set; }
}
