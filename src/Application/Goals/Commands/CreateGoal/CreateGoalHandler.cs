using Application.Contracts.Responses;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Goals.Commands.CreateGoal
{
    public sealed class CreateGoalHandler : IRequestHandler<CreateGoalCommand, GoalDto>
    {
        private readonly IGoalRepository _repo;
        public CreateGoalHandler(IGoalRepository repo) => _repo = repo;

        public async Task<GoalDto> Handle(CreateGoalCommand cmd, CancellationToken ct)
        {
            var r = cmd.Request;

            var freq = (r.FrequencyTimes.HasValue && r.FrequencyPer.HasValue)
                ? new Frequency(r.FrequencyTimes.Value, r.FrequencyPer.Value)
                : null;

            var goal = Goal.Create(
                cmd.UserId, r.Title, r.Type,
                new Target(r.TargetValue, r.TargetUnit),
                new Period(r.StartDate, r.EndDate),
                freq, r.Current);

            await _repo.AddAsync(goal, ct);
            await _repo.SaveChangesAsync(ct);

            return new GoalDto(
                goal.Id, goal.Title, goal.Type,
                goal.Target.Value, goal.Target.Unit,
                goal.Period.StartDate, goal.Period.EndDate,
                goal.Frequency?.Times, goal.Frequency?.Per,
                goal.Current, goal.Status,
                goal.CreatedAtUtc, goal.UpdatedAtUtc);
        }
    }
}
