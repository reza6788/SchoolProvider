using AutoMapper;
using MediatR;
using SchoolProvider.Business.ClassRoom.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.ClassRoom;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.ClassRoom.Handlers;

public class GetAllClassRoomQueryHandler : IRequestHandler<GetAllClassRoomQuery, ResultModel<List<ClassRoomDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllClassRoomQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<List<ClassRoomDto>>> Handle(GetAllClassRoomQuery request,
        CancellationToken cancellationToken)
    {
        var classRoomEntities = await _unitOfWork.ClassRooms.GetAllAsync();
        var classRooms = _mapper.Map<List<ClassRoomDto>>(classRoomEntities);
        return classRooms.AsSuccess();
    }
}