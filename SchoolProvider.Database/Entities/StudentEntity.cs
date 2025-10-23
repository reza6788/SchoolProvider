namespace SchoolProvider.Database.Entities;

public class StudentEntity : BaseEntity 
{
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; } =string.Empty;
    public string Address { get; set; } =string.Empty;
    public List<string> Courses { get; set; } = new();
}