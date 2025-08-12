namespace Domain.ValueObjects
{
    public sealed record Period(DateOnly StartDate, DateOnly EndDate)
    {
        public bool Contains(DateOnly date) => date >= StartDate && date <= EndDate;
    }
}
