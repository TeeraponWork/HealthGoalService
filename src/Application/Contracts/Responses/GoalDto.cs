using Domain.Enums;

namespace Application.Contracts.Responses
{
    public sealed record GoalDto(
    Guid Id, string Title, GoalType Type,
    decimal TargetValue, Unit TargetUnit,
    DateOnly StartDate, DateOnly EndDate,
    int? FrequencyTimes, FrequencyPer? FrequencyPer,
    decimal? Current, GoalStatus Status,
    DateTime CreatedAtUtc, DateTime? UpdatedAtUtc);
}
