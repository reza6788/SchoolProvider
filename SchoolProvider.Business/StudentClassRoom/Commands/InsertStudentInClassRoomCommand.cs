using MediatR;
using SchoolProvider.Contract.Common;

namespace SchoolProvider.Business.StudentClassRoom.Commands;

public record InsertStudentInClassRoomCommand (string ClassRoomId, string StudentId) : IRequest<ResultModel<bool>>;