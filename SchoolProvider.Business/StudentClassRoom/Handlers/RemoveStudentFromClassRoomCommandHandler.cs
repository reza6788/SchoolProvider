using System.Net;
using MediatR;
using SchoolProvider.Business.StudentClassRoom.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.StudentClassRoom.Handlers;

public class
    RemoveStudentFromClassRoomCommandHandler : IRequestHandler<RemoveStudentFromClassRoomCommand, ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveStudentFromClassRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultModel<bool>> Handle(RemoveStudentFromClassRoomCommand request,
        CancellationToken cancellationToken)
    {
        var studentInfo = await _unitOfWork.Students.GetByIdAsync(request.StudentId);
        if (studentInfo == null) return false.AsWarning("Student not found", HttpStatusCode.NotFound);
        var classRoomInfo = await _unitOfWork.ClassRooms.GetByIdAsync(request.ClassRoomId);
        if (classRoomInfo == null) return false.AsWarning("Class room not found", HttpStatusCode.NotFound);

        var studentClassRoomInfo = await _unitOfWork.StudentClassRooms.GetByFilterAsync(p =>
            !p.IsDeleted && p.ClassRoomId == request.ClassRoomId && p.StudentId == request.StudentId);
        var studentClassRoomEntities = studentClassRoomInfo.ToList();
        if (studentClassRoomEntities.Count == 0)
            return false.AsWarning("Not found student in current class room", HttpStatusCode.NotFound);

        foreach (var studentClassRoom in studentClassRoomEntities)
        {
            await _unitOfWork.StudentClassRooms.DeleteAsync(studentClassRoom.Id, studentClassRoom);
        }

        return true.AsSuccess("Student removed from class room successfully");
    }
}