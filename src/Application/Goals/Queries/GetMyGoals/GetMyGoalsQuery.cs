using Application.Contracts.Responses;
using MediatR;

namespace Application.Goals.Queries.GetMyGoals
{
    public sealed record GetMyGoalsQuery(Guid UserId, int Page, int PageSize)
    : IRequest<IReadOnlyList<GoalDto>>;
}
