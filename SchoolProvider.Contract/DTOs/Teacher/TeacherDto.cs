namespace SchoolProvider.Contract.DTOs.Teacher;

public class TeacherDto
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime CreateDateTime { get; set; }
    public DateTime? LastChangeDateTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDateTime { get; set; }
}