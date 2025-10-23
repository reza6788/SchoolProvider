namespace SchoolProvider.Database.Entities;

public class CourseEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}