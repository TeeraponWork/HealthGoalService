using Api.Security;
using Application.Contracts.Requests;
using Application.Contracts.Responses;
using Application.Goals.Commands.CreateGoal;
using Application.Goals.Queries.GetGoalById;
using Application.Goals.Queries.GetMyGoals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController(IMediator mediator, IUserContext user) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GoalDto>> Create([FromBody] CreateGoalRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateGoalCommand(user.UserId, req), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("my")]
    public async Task<ActionResult<IReadOnlyList<GoalDto>>> GetMy(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetMyGoalsQuery(user.UserId, page, pageSize), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GoalDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetGoalByIdQuery(user.UserId, id), ct);
        return result is null ? NotFound() : Ok(result);
    }
}
