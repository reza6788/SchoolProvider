namespace SchoolProvider.Database.Entities;

public class StudentEntity : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public List<string> Courses { get; set; } = new();
}