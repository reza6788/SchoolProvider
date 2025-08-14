using System.Net;
using MediatR;
using SchoolProvider.Business.ClassRoom.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.ClassRoom.Handlers;

public class DeleteClassRoomCommandHandler : IRequestHandler<DeleteClassRoomCommand, ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClassRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ResultModel<bool>> Handle(DeleteClassRoomCommand request, CancellationToken cancellationToken)
    {
        var classRoom = await _unitOfWork.ClassRooms.GetByIdAsync(request.ClassRoomId);
        if(classRoom == null)  return false.AsWarning("Class room not found",HttpStatusCode.NotFound);

        await _unitOfWork.ClassRooms.DeleteAsync(request.ClassRoomId, classRoom);
        return true.AsSuccess("Class room deleted");
    }
}