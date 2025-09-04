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
    [ProducesResponseType(typeof(ResultModel<List<ClassRoomDto>>), StatusCodes.Status200OK)]
    public async Task<ResultModel<List<ClassRoomDto>>> GetAll()
    {
        return await _mediator.Send(new GetAllClassRoomQuery());
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultModel<ClassRoomDto>), StatusCodes.Status200OK)]
    public async Task<ResultModel<ClassRoomDto>> GetById(string classRoomId)
    {
        return await _mediator.Send(new GetClassRoomByIdQuery(classRoomId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultModel<ClassRoomDto>), StatusCodes.Status200OK)]
    public async Task<ResultModel<ClassRoomDto>> Create(ClassRoomCreateDto classRoom)
    {
        return await _mediator.Send(new CreateClassRoomCommand(classRoom));
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<ResultModel<bool>> Update(ClassRoomUpdateDto classRoom)
    {
        return await _mediator.Send(new UpdateClassRoomCommand(classRoom));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    public async Task<ResultModel<bool>> Delete(string id)
    {
        return await _mediator.Send(new DeleteClassRoomCommand(id));
    }
}