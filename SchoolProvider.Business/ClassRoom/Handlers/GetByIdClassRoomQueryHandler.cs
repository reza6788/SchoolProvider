using System.Net;
using AutoMapper;
using MediatR;
using SchoolProvider.Business.ClassRoom.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.ClassRoom;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.ClassRoom.Handlers;

public class GetByIdClassRoomQueryHandler : IRequestHandler<GetClassRoomByIdQuery, ResultModel<ClassRoomDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdClassRoomQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<ClassRoomDto>> Handle(GetClassRoomByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = new ClassRoomDto();
        var classRoomEntity = await _unitOfWork.ClassRooms.GetByIdAsync(request.Id);
        if (classRoomEntity == null) return result.AsWarning("Class room not found", HttpStatusCode.NotFound);
        var classRooms = _mapper.Map<ClassRoomDto>(classRoomEntity);
        return classRooms.AsSuccess();
    }
}