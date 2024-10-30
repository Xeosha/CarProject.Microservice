
using CatalogService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using ShareDTO;

namespace CatalogService.Infrastructure.Repositories
{

    public class DailyWorkingHoursRepository : IDailyWorkingHoursRepository
    {
        private readonly CatalogServiceDbContext _context;

        public DailyWorkingHoursRepository(CatalogServiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkingHoursDto>> GetAllTimes(Guid serviceOrgId)
        {
            var weeklyHours = await _context.ServiceOrgDailys
                 .Where(sod => sod.ServiceOrgId == serviceOrgId)
                 .Join(_context.DailyWorkingHours,
                     sod => sod.DailyId,
                     dw => dw.Id,
                     (sod, dw) => new { sod, dw })
                 .Join(_context.TimeOfDailys,
                     combined => combined.dw.IdTime,
                     tod => tod.Id,
                     (combined, tod) => new
                     {
                         DayOfWeek = combined.dw.DayOfWeek,
                         StartTime = tod.StartTime,
                         EndTime = tod.EndTime
                     })
                 .GroupBy(d => d.DayOfWeek)
                 .Select(g => new WorkingHoursDto
                 {
                     DayOfWeek = g.Key,
                     TimeSlots = g.Select(t => new TimeSlotDto
                     {
                         StartTime = t.StartTime,
                         EndTime = t.EndTime
                     }).ToList()
                 })
                 .OrderBy(d => d.DayOfWeek)
                 .ToListAsync();

            return weeklyHours;
        }
    }

}

