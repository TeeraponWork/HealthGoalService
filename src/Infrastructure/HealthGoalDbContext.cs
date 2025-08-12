using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class HealthGoalDbContext : DbContext
    {
        public HealthGoalDbContext(DbContextOptions<HealthGoalDbContext> options) : base(options) { }

        public DbSet<Goal> Goals => Set<Goal>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Goal>(e =>
            {
                e.ToTable("goals");

                // Columns on goals
                e.Property(x => x.Id).HasColumnName("id");
                e.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
                e.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
                e.Property(x => x.Type).HasColumnName("type").HasConversion<string>().IsRequired();
                e.Property(x => x.Status).HasColumnName("status").HasConversion<string>().IsRequired();
                e.Property(x => x.Current).HasColumnName("current").HasPrecision(18, 2);
                e.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
                e.Property(x => x.UpdatedAtUtc).HasColumnName("updated_at_utc");

                // Value Objects
                e.OwnsOne(x => x.Target, t =>
                {
                    t.Property(p => p.Value).HasColumnName("target_value").HasPrecision(18, 2);
                    t.Property(p => p.Unit).HasColumnName("target_unit").HasConversion<string>();
                });

                e.OwnsOne(x => x.Period, p =>
                {
                    p.Property(pp => pp.StartDate).HasColumnName("start_date");
                    p.Property(pp => pp.EndDate).HasColumnName("end_date");
                });

                e.OwnsOne(x => x.Frequency, f =>
                {
                    f.Property(pp => pp.Times).HasColumnName("freq_times");
                    f.Property(pp => pp.Per).HasColumnName("freq_per").HasConversion<string>();
                });

                // Owned collection: goal_progress
                e.OwnsMany(x => x.ProgressEntries, p =>
                {
                    p.ToTable("goal_progress");
                    p.WithOwner().HasForeignKey("goal_id");

                    p.HasKey(pe => pe.Id);
                    p.Property(pe => pe.Id).HasColumnName("id");

                    p.Property(pe => pe.Date).HasColumnName("date");
                    p.Property(pe => pe.Amount).HasColumnName("amount").HasPrecision(18, 2);
                    p.Property(pe => pe.Note).HasColumnName("note").HasMaxLength(500);
                });

                // สำคัญ: ให้ EF ใช้ backing field "_progress" แทน property ที่เป็น ReadOnly
                e.Navigation(g => g.ProgressEntries)
                 .HasField("_progress")
                 .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            base.OnModelCreating(b);
        }
    }
}
