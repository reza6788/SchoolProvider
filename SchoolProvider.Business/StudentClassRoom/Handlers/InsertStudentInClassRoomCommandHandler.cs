using System.Net;
using MediatR;
using SchoolProvider.Business.StudentClassRoom.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.Entities;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.StudentClassRoom.Handlers;

public class InsertStudentInClassRoomCommandHandler : IRequestHandler<InsertStudentInClassRoomCommand, ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public InsertStudentInClassRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResultModel<bool>> Handle(InsertStudentInClassRoomCommand request, CancellationToken cancellationToken)
    {
        var studentInfo=await _unitOfWork.Students.GetByIdAsync(request.StudentId);
        if (studentInfo == null) return false.AsWarning("Student not found",HttpStatusCode.NotFound);
        var classRoomInfo=await _unitOfWork.ClassRooms.GetByIdAsync(request.ClassRoomId);
        if (classRoomInfo == null) return false.AsWarning("Class room not found",HttpStatusCode.NotFound);
        
        var studentClassRoom = new StudentClassRoomEntity
        {
            ClassRoomId = request.ClassRoomId,
            StudentId = request.StudentId
        };
        await _unitOfWork.StudentClassRooms.AddAsync(studentClassRoom);
        return true.AsSuccess("Student inserted into class room successfully");
    }
}