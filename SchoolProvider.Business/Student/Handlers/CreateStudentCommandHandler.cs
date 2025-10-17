using AutoMapper;
using MediatR;
using SchoolProvider.Business.Student.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Student;
using SchoolProvider.Database.Entities;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Student.Handlers;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, ResultModel<StudentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateStudentCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new StudentEntity
        {
            FullName = request.Student.FullName,
            DateOfBirth = request.Student.DateOfBirth,
            Age = request.Student.Age,
            PhoneNumber = request.Student.PhoneNumber,
            Courses = request.Student.Courses,
        };

        var newStudentEntity =await _unitOfWork.Students.AddAsync(student);
        var newStudent=_mapper.Map<StudentDto>(newStudentEntity);
        return newStudent.AsSuccess("student created");
    }
}