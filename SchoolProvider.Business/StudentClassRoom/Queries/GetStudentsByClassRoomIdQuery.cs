using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs;

namespace SchoolProvider.Business.StudentClassRoom.Queries;

public record GetStudentsByClassRoomIdQuery(string ClassRoomId) : IRequest<ResultModel<ClassRoomWithStudentsDto>>;
