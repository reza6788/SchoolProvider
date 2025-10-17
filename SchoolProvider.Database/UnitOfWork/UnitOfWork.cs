using SchoolProvider.Database.Context;
using SchoolProvider.Database.Entities;
using SchoolProvider.Database.Repository;

namespace SchoolProvider.Database.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMongoDbContext _context;
    private IRepository<StudentEntity>? _students;
    private IRepository<TeacherEntity>? _teachers;
    private IRepository<ClassRoomEntity>? _classRooms;
    private IRepository<StudentClassRoomEntity>? _stundetClassRooms;

    public UnitOfWork(IMongoDbContext context)
    {
        _context = context;
    }
    public IRepository<StudentEntity> Students => _students ??= new Repository<StudentEntity>(_context);
    public IRepository<TeacherEntity> Teachers => _teachers ??= new Repository<TeacherEntity>(_context);
    public IRepository<ClassRoomEntity> ClassRooms => _classRooms ??= new Repository<ClassRoomEntity>(_context);
    public IRepository<StudentClassRoomEntity> StudentClassRooms => _stundetClassRooms ??= new Repository<StudentClassRoomEntity>(_context);
}