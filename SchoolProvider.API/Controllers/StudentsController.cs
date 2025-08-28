using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProvider.Business.Student.Commands;
using SchoolProvider.Business.Student.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs;
using SchoolProvider.Contract.DTOs.Student;

namespace SchoolProvider.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ResultModel<List<StudentDto>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllStudentsQuery());
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ResultModel<StudentDto>>> GetById(string studentId)
    {
        return await _mediator.Send(new GetStudentByIdQuery(studentId));
    }

    [HttpPost]
    public async Task<ActionResult<ResultModel<StudentDto>>> Create(StudentCreateDto student)
    {
        return await _mediator.Send(new CreateStudentCommand(student));
    }

    [HttpPut]
    public async Task<ActionResult<ResultModel<bool>>> Update(StudentUpdateDto student)
    {
        return await _mediator.Send(new UpdateStudentCommand(student));
    }

    [HttpDelete]
    public async Task<ActionResult<ResultModel<bool>>> Delete(string id)
    {
        return await _mediator.Send(new DeleteStudentCommand(id));
    }
}