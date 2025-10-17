using System.Net;
using MediatR;
using SchoolProvider.Business.Teacher.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Teacher.Handlers;

public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand,ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTeacherCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultModel<bool>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.Teachers.GetByIdAsync(request.Id);
        if (existing == null) return false.AsWarning("teacher not found",HttpStatusCode.NotFound);
        
        await _unitOfWork.Teachers.DeleteAsync(request.Id,existing);
        return true.AsSuccess("teacher deleted");
    }
}