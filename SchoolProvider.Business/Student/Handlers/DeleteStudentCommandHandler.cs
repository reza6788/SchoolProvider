using System.Net;
using MediatR;
using SchoolProvider.Business.Student.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Student.Handlers;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand,ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultModel<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.Students.GetByIdAsync(request.Id);
        if (existing == null) return false.AsWarning("student not found",HttpStatusCode.NotFound);
        
        await _unitOfWork.Students.DeleteAsync(request.Id,existing);
        return true.AsSuccess("student deleted");
    }
}