using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Goal
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public GoalType Type { get; private set; }
        public Target Target { get; private set; }
        public Period Period { get; private set; }
        public Frequency? Frequency { get; private set; }
        public decimal? Current { get; private set; }
        public GoalStatus Status { get; private set; } = GoalStatus.Active;
        private readonly List<ProgressEntry> _progress = new();
        public IReadOnlyCollection<ProgressEntry> ProgressEntries => _progress.AsReadOnly();
        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; private set; }

        private Goal() { }

        public static Goal Create(Guid userId, string title, GoalType type, Target target, Period period, Frequency? frequency, decimal? current)
        {
            if (period.EndDate < period.StartDate) throw new ArgumentException("EndDate must be >= StartDate");
            return new Goal
            {
                UserId = userId,
                Title = title,
                Type = type,
                Target = target,
                Period = period,
                Frequency = frequency,
                Current = current
            };
        }

        public void Update(string title, Target target, Period period, Frequency? frequency, decimal? current)
        {
            Title = title;
            Target = target;
            Period = period;
            Frequency = frequency;
            Current = current;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public ProgressEntry AddProgress(DateOnly date, decimal amount, string? note)
        {
            if (!Period.Contains(date)) throw new InvalidOperationException("Progress date is out of goal period");
            var entry = new ProgressEntry(date, amount, note);
            _progress.Add(entry);
            UpdatedAtUtc = DateTime.UtcNow;
            return entry;
        }

        public void Cancel()
        {
            Status = GoalStatus.Cancelled;
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
