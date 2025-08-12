using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IGoalRepository
    {
        Task AddAsync(Goal goal, CancellationToken ct);
        Task<Goal?> GetByIdAsync(Guid id, Guid ownerUserId, CancellationToken ct);
        Task<IReadOnlyList<Goal>> GetByUserAsync(Guid ownerUserId, int page, int pageSize, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
