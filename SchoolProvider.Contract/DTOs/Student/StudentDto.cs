namespace SchoolProvider.Contract.DTOs.Student;

public class StudentDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int AgeInYear { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public List<string> Courses { get; set; } = new();
    public DateTime CreateDateTime { get; set; }
    public DateTime? LastChangeDateTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDateTime { get; set; }
}