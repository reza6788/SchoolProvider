using AutoMapper;
using MediatR;
using SchoolProvider.Business.Student.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Student;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Student.Handlers;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, ResultModel<List<StudentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllStudentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<List<StudentDto>>> Handle(GetAllStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var studentEntities = await _unitOfWork.Students.GetAllAsync();
        var students = _mapper.Map<List<StudentDto>>(studentEntities);
        return students.AsSuccess();
    }
}