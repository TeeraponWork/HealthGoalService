using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly HealthGoalDbContext _db;
        public GoalRepository(HealthGoalDbContext db) => _db = db;

        public Task AddAsync(Goal goal, CancellationToken ct) => _db.Goals.AddAsync(goal, ct).AsTask();

        public Task<Goal?> GetByIdAsync(Guid id, Guid ownerUserId, CancellationToken ct) =>
            _db.Goals
               .Where(g => g.Id == id && g.UserId == ownerUserId)
               .Include(g => g.ProgressEntries)
               .SingleOrDefaultAsync(ct);

        public async Task<IReadOnlyList<Goal>> GetByUserAsync(Guid ownerUserId, int page, int pageSize, CancellationToken ct)
        {
            return await _db.Goals
                .Where(g => g.UserId == ownerUserId)
                .OrderByDescending(g => g.CreatedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
    }
}
