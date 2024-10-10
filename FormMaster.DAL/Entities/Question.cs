namespace FormMaster.DAL.Entities;

public class Question
{
    public int QuestionId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public QuestionType QuestionType { get; set; }
    public bool IsVisible { get; set; }
    public int TemplateId { get; set; }
    public Template? Template { get; set; }
}
