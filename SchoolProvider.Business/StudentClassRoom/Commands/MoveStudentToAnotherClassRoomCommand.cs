using MediatR;
using SchoolProvider.Contract.Common;

namespace SchoolProvider.Business.StudentClassRoom.Commands;

public record MoveStudentToAnotherClassRoomCommand(string FirstClassRoomId, string SecondClassRoomId, string StudentId)
    : IRequest<ResultModel<bool>>;