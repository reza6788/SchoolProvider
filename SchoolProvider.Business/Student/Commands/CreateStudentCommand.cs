using MediatR;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Student;

namespace SchoolProvider.Business.Student.Commands;

public record CreateStudentCommand(StudentCreateDto Student) : IRequest<ResultModel<StudentDto>>;
