namespace SchoolProvider.Contract.DTOs.Student;

public class StudentDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public List<string> Courses { get; set; } = new();
    public DateTime CreateDateTime { get; set; }
    public DateTime? LastChangeDateTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDateTime { get; set; }
}