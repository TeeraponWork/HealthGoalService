using Application.Contracts.Requests;
using Application.Contracts.Responses;
using MediatR;

namespace Application.Goals.Commands.CreateGoal
{
    public sealed record CreateGoalCommand(Guid UserId, CreateGoalRequest Request) : IRequest<GoalDto>;
}
