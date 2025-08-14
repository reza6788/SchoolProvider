using System.Net;
using AutoMapper;
using MediatR;
using SchoolProvider.Business.Student.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Student;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Student.Handlers;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, ResultModel<StudentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStudentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new StudentDto();
        var student = await _unitOfWork.Students.GetByIdAsync(request.Id);
        if (student is null) return result.AsWarning("Student not found", HttpStatusCode.NotFound);
        result = _mapper.Map<StudentDto>(student);
        return result.AsSuccess();
    }
}