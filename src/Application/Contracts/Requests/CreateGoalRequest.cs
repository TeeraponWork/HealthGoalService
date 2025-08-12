using Domain.Enums;

namespace Application.Contracts.Requests
{
    public sealed record CreateGoalRequest(
        string Title,
        GoalType Type,
        decimal TargetValue,
        Unit TargetUnit,
        DateOnly StartDate,
        DateOnly EndDate,
        int? FrequencyTimes,
        FrequencyPer? FrequencyPer,
        decimal? Current
    );
}
