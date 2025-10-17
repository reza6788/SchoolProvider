using AutoMapper;
using MediatR;
using SchoolProvider.Business.Teacher.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Teacher;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Teacher.Handlers;

public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, ResultModel<List<TeacherDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllTeachersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<List<TeacherDto>>> Handle(GetAllTeachersQuery request,
        CancellationToken cancellationToken)
    {
        var teacherEntities = await _unitOfWork.Teachers.GetAllAsync();
        var teachers = _mapper.Map<List<TeacherDto>>(teacherEntities);
        return teachers.AsSuccess();
    }
}