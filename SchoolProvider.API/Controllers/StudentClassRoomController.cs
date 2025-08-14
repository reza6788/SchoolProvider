using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProvider.Business.StudentClassRoom.Queries;
using SchoolProvider.Business.StudentClassRoom.Commands;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs;

namespace SchoolProvider.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StudentClassRoomController : Controller
{
    private readonly IMediator _mediator;

    public StudentClassRoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultModel<ClassRoomWithStudentsDto>), StatusCodes.Status200OK)]
    
    public async Task<IActionResult> GetStudentsByClassroomId(string classRoomId)
    {
        var result = await _mediator.Send(new GetStudentsByClassRoomIdQuery(classRoomId));
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> InsertStudentInClassRoom(string classRoomId, string studentId)
    {
        var result = await _mediator.Send(new InsertStudentInClassRoomCommand(classRoomId,studentId));
        return Ok(result);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> InsertStudentInClassRoom(string firstClassRoomId, 
        string secondClassRoomId, string studentId)
    {
        var result = await _mediator.Send(new MoveStudentToAnotherClassRoomCommand(firstClassRoomId,secondClassRoomId,studentId));
        return Ok(result);
    }
    
    [HttpDelete]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveStudentFromClassRoom(string classRoomId, string studentId)
    {
        var result = await _mediator.Send(new RemoveStudentFromClassRoomCommand(classRoomId,studentId));
        return Ok(result);
    }
}