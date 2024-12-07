using ShareDTO;

namespace CatalogService.Domain.Interfaces
{
    public interface IScheduleService
    {
        public Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId);
    }
}
