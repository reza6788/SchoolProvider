using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Teacher;

namespace SchoolProvider.Business.Teacher.Commands;

public record UpdateTeacherCommand(TeacherUpdateDto Teacher) : IRequest<ResultModel<bool>>;