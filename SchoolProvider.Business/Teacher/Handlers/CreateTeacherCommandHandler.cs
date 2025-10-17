using AutoMapper;
using MediatR;
using SchoolProvider.Business.Teacher.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Teacher;
using SchoolProvider.Database.Entities;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Teacher.Handlers;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, ResultModel<TeacherDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTeacherCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<TeacherDto>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = new TeacherEntity
        {
            FirstName = request.Teacher.FirstName,
            LastName = request.Teacher.LastName,
            Email = request.Teacher.Email,
            PhoneNumber = request.Teacher.PhoneNumber,
        };

        var newTeacherEntity = await _unitOfWork.Teachers.AddAsync(teacher);
        var newTeacher = _mapper.Map<TeacherDto>(newTeacherEntity);
        return newTeacher.AsSuccess("teacher created");
    }
}