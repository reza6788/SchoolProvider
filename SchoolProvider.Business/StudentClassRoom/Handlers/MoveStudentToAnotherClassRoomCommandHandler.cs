using System.Net;
using MediatR;
using SchoolProvider.Business.StudentClassRoom.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.Entities;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.StudentClassRoom.Handlers;

public class MoveStudentToAnotherClassRoomCommandHandler : IRequestHandler<MoveStudentToAnotherClassRoomCommand, ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public MoveStudentToAnotherClassRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultModel<bool>> Handle(MoveStudentToAnotherClassRoomCommand request, CancellationToken cancellationToken)
    {
        var studentInfo = await _unitOfWork.Students.GetByIdAsync(request.StudentId);
        if (studentInfo == null) return false.AsWarning("Student not found",HttpStatusCode.NotFound);
        var firstClassRoomInfo = await _unitOfWork.ClassRooms.GetByIdAsync(request.FirstClassRoomId);
        if (firstClassRoomInfo == null) return false.AsWarning("First class room not found", HttpStatusCode.NotFound);
        
        var secondClassRoomId = await _unitOfWork.ClassRooms.GetByIdAsync(request.SecondClassRoomId);
        if (secondClassRoomId == null) return false.AsWarning("Second class room not found", HttpStatusCode.NotFound);
        
        // remove from first ClassRoom
        var studentClassRoomInfo = await _unitOfWork.StudentClassRooms.GetByFilterAsync(p =>
            !p.IsDeleted && p.ClassRoomId == request.FirstClassRoomId && p.StudentId == request.StudentId);
        var studentClassRoomEntities = studentClassRoomInfo.ToList();
        if (studentClassRoomEntities.Count == 0) return false.AsWarning("Not found student in current class room",HttpStatusCode.NotFound);

        foreach (var studentClassRoom in studentClassRoomEntities)
        {
            await _unitOfWork.StudentClassRooms.DeleteAsync(studentClassRoom.Id, studentClassRoom);
        }
        // Insert to another ClassRoom
        var newStudentClassRoomEntity = new StudentClassRoomEntity
        {
            ClassRoomId = request.SecondClassRoomId,
            StudentId = request.StudentId
        };
        await _unitOfWork.StudentClassRooms.AddAsync(newStudentClassRoomEntity);

        return true.AsSuccess("Student moved in new class room successfully");
    }
}