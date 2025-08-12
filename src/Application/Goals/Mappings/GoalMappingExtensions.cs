using Application.Contracts.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Goals.Mappings
{
    public static class GoalMappingExtensions
    {
        public static GoalDto ToDto(this Goal g)
            => new GoalDto(
                g.Id,
                g.Title,
                g.Type,
                g.Target.Value,
                g.Target.Unit,
                g.Period.StartDate,
                g.Period.EndDate,
                g.Frequency?.Times,
                g.Frequency?.Per,
                g.Current,
                g.Status,
                g.CreatedAtUtc,
                g.UpdatedAtUtc
            );
    }
}
