using Application.Contracts.Responses;
using MediatR;

namespace Application.Goals.Queries.GetGoalById
{
    public sealed record GetGoalByIdQuery(Guid UserId, Guid GoalId) : IRequest<GoalDto?>;
}
