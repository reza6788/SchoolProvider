using SchoolProvider.Database.Entities;
using SchoolProvider.Database.Repository;

namespace SchoolProvider.Database.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<StudentEntity> Students { get; }
    IRepository<ClassRoomEntity> ClassRooms { get; }
    IRepository<StudentClassRoomEntity> StudentClassRooms { get; }
}