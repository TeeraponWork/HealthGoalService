using Application.Contracts.Responses;
using Application.Goals.Mappings;
using Domain.Abstractions;
using MediatR;

namespace Application.Goals.Queries.GetMyGoals
{
    public sealed class GetMyGoalsHandler : IRequestHandler<GetMyGoalsQuery, IReadOnlyList<GoalDto>>
    {
        private readonly IGoalRepository _repo;
        public GetMyGoalsHandler(IGoalRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<GoalDto>> Handle(GetMyGoalsQuery request, CancellationToken ct)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var size = request.PageSize <= 0 ? 20 : request.PageSize;

            var goals = await _repo.GetByUserAsync(request.UserId, page, size, ct);
            return goals.Select(g => g.ToDto()).ToList();
        }
    }
}
