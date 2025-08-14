using MediatR;
using SchoolProvider.Contract.Common;

namespace SchoolProvider.Business.Student.Commands;

public record DeleteStudentCommand(string Id) : IRequest<ResultModel<bool>>;