using System.Net;
using MediatR;
using SchoolProvider.Business.Teacher.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Teacher.Handlers;

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTeacherCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultModel<bool>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.Teachers.GetByIdAsync(request.Teacher.Id);

        if (existing == null) return false.AsWarning("Student not found", HttpStatusCode.NotFound);
        existing.FirstName = request.Teacher.FirstName;
        existing.LastName = request.Teacher.LastName;
        existing.Email = request.Teacher.Email;
        existing.PhoneNumber = request.Teacher.PhoneNumber;

        await _unitOfWork.Teachers.UpdateAsync(request.Teacher.Id, existing);
        return true.AsSuccess("Teacher modified");
    }
}