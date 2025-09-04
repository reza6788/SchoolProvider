using System.Net;
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
        var newClassRoom = new ClassRoomDto();
        var isClassRoomNameExist = await IsClassRoomNameExist(request.ClassRoom.Name);
        if(isClassRoomNameExist) return newClassRoom.AsWarning("Class room name already exist");
        
        var classRoom = new ClassRoomEntity
        {
            Name = request.ClassRoom.Name,
            Description = request.ClassRoom.Description,
        };

        var newClassRoomEntity=await _unitOfWork.ClassRooms.AddAsync(classRoom);
        newClassRoom=_mapper.Map<ClassRoomDto>(newClassRoomEntity);
        return newClassRoom.AsSuccess("Class room created");
    }

    private async Task<bool> IsClassRoomNameExist(string name)
    {
        var classRoom = await _unitOfWork.ClassRooms.GetByFilterAsync(p => !p.IsDeleted && p.Name == name);
        return classRoom.Any();
    }
}