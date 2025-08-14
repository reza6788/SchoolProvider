using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.ClassRoom;

namespace SchoolProvider.Business.ClassRoom.Queries;

public record GetAllClassRoomQuery : IRequest<ResultModel<List<ClassRoomDto>>>;
