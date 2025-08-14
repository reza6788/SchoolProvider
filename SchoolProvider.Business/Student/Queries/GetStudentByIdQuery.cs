using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Student;

namespace SchoolProvider.Business.Student.Queries;

public record GetStudentByIdQuery(string Id) : IRequest<ResultModel<StudentDto>>;