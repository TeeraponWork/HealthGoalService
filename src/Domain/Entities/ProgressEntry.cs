namespace Domain.Entities
{
    public class ProgressEntry
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateOnly Date { get; private set; }
        public decimal Amount { get; private set; }
        public string? Note { get; private set; }
        private ProgressEntry() { }
        public ProgressEntry(DateOnly date, decimal amount, string? note)
        {
            Date = date; Amount = amount; Note = note;
        }
    }
}
