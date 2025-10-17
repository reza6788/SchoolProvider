using MediatR;
using SchoolProvider.Contract.Common;

namespace SchoolProvider.Business.Teacher.Commands;

public record DeleteTeacherCommand(string Id) : IRequest<ResultModel<bool>>;