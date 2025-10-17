using System.Net;
using MediatR;
using SchoolProvider.Business.Student.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Database.UnitOfWork;

namespace SchoolProvider.Business.Student.Handlers;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, ResultModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStudentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultModel<bool>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.Students.GetByIdAsync(request.Student.Id);

        if (existing == null) return false.AsWarning("Student not found", HttpStatusCode.NotFound);
        existing.FullName = request.Student.FullName;
        existing.DateOfBirth = request.Student.DateOfBirth;
        existing.Age = request.Student.Age;
        existing.PhoneNumber = request.Student.PhoneNumber;
        existing.Courses = request.Student.Courses;

        await _unitOfWork.Students.UpdateAsync(request.Student.Id, existing);
        return true.AsSuccess("Student modified");
    }
}