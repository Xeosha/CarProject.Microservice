using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Repositories;
using ShareDTO;
namespace CatalogService.Application.Servicess
{
    public class ScheduleService : IScheduleService
    {
        private IDailyWorkingHoursRepository _dailyWorkingHoursRepository;

        public ScheduleService(IDailyWorkingHoursRepository dailyWorkingHoursRepository)
        {
            _dailyWorkingHoursRepository = dailyWorkingHoursRepository;
        }

        public async Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId)
        {
            return await _dailyWorkingHoursRepository.GetAllTimes(organizationServiceId);
        }

    }
}
