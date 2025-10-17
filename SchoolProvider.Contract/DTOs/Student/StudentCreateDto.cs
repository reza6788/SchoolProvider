namespace SchoolProvider.Contract.DTOs.Student;

public class StudentCreateDto
{
    public string FullName { get; set; } = string.Empty;
    public int AgeInYears { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public List<string> Courses { get; set; } = new();
}