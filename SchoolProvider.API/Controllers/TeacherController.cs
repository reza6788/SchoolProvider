using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProvider.Business.Teacher.Commands;
using SchoolProvider.Business.Teacher.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.Teacher;

namespace SchoolProvider.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultModel<List<TeacherDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllTeachersQuery());
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultModel<TeacherDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(string teacherId)
    {
        return await _mediator.Send(new GetTeacherByIdQuery(teacherId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultModel<TeacherDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(TeacherCreateDto teacher)
    {
        return await _mediator.Send(new CreateTeacherCommand(teacher));
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(TeacherUpdateDto teacher)
    {
        return await _mediator.Send(new UpdateTeacherCommand(teacher));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string id)
    {
        return await _mediator.Send(new DeleteTeacherCommand(id));
    }
}