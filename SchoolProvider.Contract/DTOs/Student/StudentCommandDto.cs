namespace SchoolProvider.Contract.DTOs.Student;

public class StudentUpdateDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public List<string> Courses { get; set; } = new();
}