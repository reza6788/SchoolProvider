using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Teacher;

namespace SchoolProvider.Business.Teacher.Queries;

public record GetTeacherByIdQuery(string Id) : IRequest<ResultModel<TeacherDto>>;