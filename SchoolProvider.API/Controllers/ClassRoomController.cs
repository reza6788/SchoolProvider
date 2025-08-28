using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProvider.Business.ClassRoom.Commands;
using SchoolProvider.Business.ClassRoom.Queries;
using SchoolProvider.Contract.Common;
using SchoolProvider.Contract.DTOs.ClassRoom;

namespace SchoolProvider.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ClassRoomController : Controller
{
    private readonly IMediator _mediator;

    public ClassRoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ResultModel<List<ClassRoomDto>>>> GetAll()
    {
        return await _mediator.Send(new GetAllClassRoomQuery());
    }

    [HttpGet]
    public async Task<ActionResult<ResultModel<ClassRoomDto>>> GetById(string classRoomId)
    {
        return await _mediator.Send(new GetClassRoomByIdQuery(classRoomId));
    }

    [HttpPost]
    public async Task<ActionResult<ResultModel<ClassRoomDto>>> Create(ClassRoomCreateDto classRoom)
    {
        return await _mediator.Send(new CreateClassRoomCommand(classRoom));
    }

    [HttpPut]
    public async Task<ActionResult<ResultModel<bool>>> Update(ClassRoomUpdateDto classRoom)
    {
        return await _mediator.Send(new UpdateClassRoomCommand(classRoom));
    }

    [HttpDelete]
    public async Task<ActionResult<ResultModel<bool>>> Delete(string id)
    {
        return await _mediator.Send(new DeleteClassRoomCommand(id));
    }
}