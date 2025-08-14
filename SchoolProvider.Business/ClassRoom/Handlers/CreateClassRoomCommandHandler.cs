using AutoMapper;
using MediatR;
using SchoolProvider.Business.ClassRoom.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.ClassRoom;
using SchoolProvider.Database.Entities;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.ClassRoom.Handlers;

public class CreateClassRoomCommandHandler : IRequestHandler<CreateClassRoomCommand, ResultModel<ClassRoomDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateClassRoomCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultModel<ClassRoomDto>> Handle(CreateClassRoomCommand request, CancellationToken cancellationToken)
    {
        var classRoom = new ClassRoomEntity
        {
            Name = request.ClassRoom.Name,
            Description = request.ClassRoom.Description,
        };

        var newClassRoomEntity=await _unitOfWork.ClassRooms.AddAsync(classRoom);
        var newClassRoom=_mapper.Map<ClassRoomDto>(newClassRoomEntity);
        return newClassRoom.AsSuccess("Class room created");
    }
}