namespace FormMaster.DAL.Entities;

public class Answer
{
    public int AnswerId { get; set; }
    public string? Name { get; set; }
    public int FormId { get; set; }
    public Form? Form { get; set; }
}
