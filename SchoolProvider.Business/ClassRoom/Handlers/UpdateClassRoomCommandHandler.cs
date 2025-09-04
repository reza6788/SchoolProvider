using System.Net;
using MediatR;
using SchoolProvider.Business.ClassRoom.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.ClassRoom.Handlers;

public class UpdateClassRoomCommandHandler : IRequestHandler<UpdateClassRoomCommand, ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClassRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultModel<bool>> Handle(UpdateClassRoomCommand request, CancellationToken cancellationToken)
    {
        var classRoom = await _unitOfWork.ClassRooms.GetByIdAsync(request.ClassRoom.Id);
        if (classRoom == null) return false.AsWarning("Class room not found", HttpStatusCode.NotFound);
        
        var isClassRoomNameDuplicate = await IsClassRoomNameDuplicate(request.ClassRoom.Id, request.ClassRoom.Name);
        if (isClassRoomNameDuplicate) return false.AsWarning($"Class room name {request.ClassRoom.Name} already exist");

        classRoom.Name = request.ClassRoom.Name;
        classRoom.Description = request.ClassRoom.Description;

        await _unitOfWork.ClassRooms.UpdateAsync(request.ClassRoom.Id, classRoom);
        return true.AsSuccess("Class room modified");
    }

    private async Task<bool> IsClassRoomNameDuplicate(string id, string name)
    {
        var classRoom = await _unitOfWork.ClassRooms.GetByFilterAsync(p =>
            !p.IsDeleted && p.Id != id && p.Name == name);
        return classRoom.Any();
    }
}