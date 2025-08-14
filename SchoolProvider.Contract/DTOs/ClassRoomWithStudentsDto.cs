using SchoolProvider.Contract.DTOs.Student;

namespace SchoolProvider.Contract.DTOs;

public class ClassRoomWithStudentsDto
{
    public string ClassRoomId { get; set; } = string.Empty;
    public string ClassRoomName { get; set; } =string.Empty;
    public List<StudentDto> Students { get; set; } = null!;
}