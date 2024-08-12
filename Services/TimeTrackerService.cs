using CrudSystem.Data;
using CrudSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudSystem.Services
{
    public class TimeTrackerService
    {
        private readonly ApplicationDbContext _dbContext;

        public TimeTrackerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ValidateTimeTrackerAsync(TimeTracker newTracker)
        {
            var overlappingTrackers = await _dbContext.TimeTrackers
                .Where(tt => tt.CollaboratorId == newTracker.CollaboratorId
                             && tt.TarefasId == newTracker.TarefasId
                             && ((newTracker.StartTime >= tt.StartTime && newTracker.StartTime < tt.EndTime)
                             || (newTracker.EndTime > tt.StartTime && newTracker.EndTime <= tt.EndTime)))
                .ToListAsync();

            return !overlappingTrackers.Any();
        }

        public async Task<bool> ValidateTotalHoursAsync(Guid collaboratorId, DateTime date)
        {
            var totalHours = await _dbContext.TimeTrackers
                .Where(tt => tt.CollaboratorId == collaboratorId
                             && tt.StartTime.Date == date.Date)
                .SumAsync(tt => tt.GetDuration().TotalHours);

            return totalHours <= 24;
        }

    }
}
