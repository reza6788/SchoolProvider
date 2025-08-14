namespace SchoolProvider.Contract.DTOs.Student;

public class StudentCreateDto
{
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public List<string> Courses { get; set; } = new();
}