using AutoMapper;
using SchoolProvider.Contract.DTOs.ClassRoom;
using SchoolProvider.Contract.DTOs.Student;
using SchoolProvider.Database.Entities;

namespace SchoolProvider.Business.Mapping;

public class MappingProfile :Profile
{
    public MappingProfile()
    {
        CreateMap<StudentEntity, StudentDto>();
        CreateMap<ClassRoomEntity, ClassRoomDto>();
    }
}