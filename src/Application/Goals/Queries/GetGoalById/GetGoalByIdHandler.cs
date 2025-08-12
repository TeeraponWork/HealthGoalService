using Application.Contracts.Responses;
using Application.Goals.Mappings;
using Domain.Abstractions;
using MediatR;

namespace Application.Goals.Queries.GetGoalById
{
    public sealed class GetGoalByIdHandler : IRequestHandler<GetGoalByIdQuery, GoalDto?>
    {
        private readonly IGoalRepository _repo;
        public GetGoalByIdHandler(IGoalRepository repo) => _repo = repo;

        public async Task<GoalDto?> Handle(GetGoalByIdQuery request, CancellationToken ct)
        {
            var goal = await _repo.GetByIdAsync(request.GoalId, request.UserId, ct);
            return goal?.ToDto();
        }
    }
}
