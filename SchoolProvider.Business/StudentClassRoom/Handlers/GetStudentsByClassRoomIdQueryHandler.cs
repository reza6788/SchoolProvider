using System.Net;
using AutoMapper;
using MediatR;
using SchoolProvider.Business.StudentClassRoom.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs;
using SchoolProvider.Contract.DTOs.Student;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.StudentClassRoom.Handlers;

public class
    GetStudentsByClassRoomIdQueryHandler : IRequestHandler<GetStudentsByClassRoomIdQuery,
    ResultModel<ClassRoomWithStudentsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStudentsByClassRoomIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<ResultModel<ClassRoomWithStudentsDto>> Handle(GetStudentsByClassRoomIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = new ClassRoomWithStudentsDto();
        var classRoomData = await _unitOfWork.ClassRooms.GetByIdAsync(request.ClassRoomId);
        if (classRoomData == null) return result.AsWarning("Class room not found", HttpStatusCode.NotFound);

        var studentClassRoomEntities = await _unitOfWork.StudentClassRooms.GetByFilterAsync(p =>
            !p.IsDeleted && p.ClassRoomId == classRoomData.Id);

        var studentIds = studentClassRoomEntities.Select(sc => sc.StudentId).ToList();

        if (studentIds.Count == 0)
        {
            // No students found for this classroom
            result = new ClassRoomWithStudentsDto
            {
                ClassRoomId = classRoomData.Id, ClassRoomName = classRoomData.Name, Students = new List<StudentDto>()
            };
            return result.AsSuccess();
        }

        // Get student entities by IDs
        var studentEntities = await _unitOfWork.Students.GetByFilterAsync(student =>
            studentIds.Contains(student.Id) && !student.IsDeleted);

        var students = _mapper.Map<List<StudentDto>>(studentEntities);

        result = new ClassRoomWithStudentsDto
            { ClassRoomId = classRoomData.Id, ClassRoomName = classRoomData.Name, Students = students };
        return result.AsSuccess();
    }
}