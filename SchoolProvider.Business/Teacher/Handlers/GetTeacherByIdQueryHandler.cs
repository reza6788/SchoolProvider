using System.Net;
using AutoMapper;
using MediatR;
using SchoolProvider.Business.Teacher.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Teacher;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Teacher.Handlers;

public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, ResultModel<TeacherDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTeacherByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<TeacherDto>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new TeacherDto();
        var teacher = await _unitOfWork.Teachers.GetByIdAsync(request.Id);
        if (teacher is null) return result.AsWarning("Teacher not found", HttpStatusCode.NotFound);
        result = _mapper.Map<TeacherDto>(teacher);
        return result.AsSuccess();
    }
}