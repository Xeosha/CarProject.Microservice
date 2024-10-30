using ShareDTO;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IDailyWorkingHoursRepository
    {
        public Task<List<WorkingHoursDto>> GetAllTimes(Guid serviceOrgId);
    }
}
