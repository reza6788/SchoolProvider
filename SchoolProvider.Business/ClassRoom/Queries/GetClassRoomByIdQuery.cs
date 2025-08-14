using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.ClassRoom;

namespace SchoolProvider.Business.ClassRoom.Queries;

public record GetClassRoomByIdQuery(string Id) : IRequest<ResultModel<ClassRoomDto>>;