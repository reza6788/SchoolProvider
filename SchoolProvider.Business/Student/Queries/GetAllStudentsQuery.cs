using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Student;

namespace SchoolProvider.Business.Student.Queries;

public record GetAllStudentsQuery : IRequest<ResultModel<List<StudentDto>>>;