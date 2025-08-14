namespace SchoolProvider.Contract.DTOs.ClassRoom;

public class ClassRoomDto
{
    public string Id { get; set; }=string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreateDateTime { get; set; }
    public DateTime? LastChangeDateTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDateTime { get; set; }
}