using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.ClassRoom;

namespace SchoolProvider.Business.ClassRoom.Commands;

public record UpdateClassRoomCommand(ClassRoomUpdateDto ClassRoom) : IRequest<ResultModel<bool>>;