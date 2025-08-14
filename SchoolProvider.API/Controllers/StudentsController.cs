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
    [ProducesResponseType(typeof(ResultModel<List<StudentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllStudentsQuery());
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultModel<StudentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(string studentId)
    {
        return await _mediator.Send(new GetStudentByIdQuery(studentId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultModel<StudentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(StudentCreateDto student)
    {
        return await _mediator.Send(new CreateStudentCommand(student));
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(StudentUpdateDto student)
    {
        return await _mediator.Send(new UpdateStudentCommand(student));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string id)
    {
        return await _mediator.Send(new DeleteStudentCommand(id));
    }
}