using MediatR;
using SchoolProvider.Contract.Common;

namespace SchoolProvider.Business.ClassRoom.Commands;

public record DeleteClassRoomCommand(string ClassRoomId) : IRequest<ResultModel<bool>>;