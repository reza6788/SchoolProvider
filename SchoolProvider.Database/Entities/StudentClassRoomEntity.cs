namespace SchoolProvider.Database.Entities;

public class StudentClassRoomEntity : BaseEntity
{
    public string StudentId { get; set; } = string.Empty;
    public string ClassRoomId { get; set; } = string.Empty;
}